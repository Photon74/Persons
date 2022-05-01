using Microsoft.IdentityModel.Tokens;

using Persons.DAL.Entities;
using Persons.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persons.Services
{
    internal sealed class UserService : IUserService
    {
        private IDictionary<string, AuthResponse> _users = new Dictionary<string, AuthResponse>()
        {
            { "test", new AuthResponse{ Password = "test" } }
        };

        public const string SecretCode = "Maternity time evaluates the natural logarithm.";
        public TokenResponse Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var tokenResponse = new TokenResponse();
            var i = 0;

            foreach (var pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, username) == 0 && string.CompareOrdinal(pair.Value.Password, password) == 0)
                {
                    tokenResponse.Token = GenerateJwtToken(i, 15);
                    var refreshToken = GenerateRefreshToken(i);
                    pair.Value.LatestRefreshToken = refreshToken;
                    tokenResponse.RefreshToken = refreshToken.Token;
                    return tokenResponse;
                }
            }
            return null;
        }

        public string RefreshToken(string token)
        {
            var i = 0;

            foreach(var pair in _users)
            {
                i++;

                if(string.CompareOrdinal(pair.Value.LatestRefreshToken.Token, token) == 0 && pair.Value.LatestRefreshToken.IsExpired is false)
                {
                    pair.Value.LatestRefreshToken = GenerateRefreshToken(i);
                    return pair.Value.LatestRefreshToken.Token;
                }
            }
            return string.Empty;
        }

        private RefreshToken GenerateRefreshToken(int id)
        {
            var refreshToken = new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360),
                Token = GenerateJwtToken(id, 360)
            };
            return refreshToken;
        }

        private string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
