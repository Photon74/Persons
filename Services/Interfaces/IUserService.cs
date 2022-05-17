using Persons.DAL.Entities;

namespace Persons.Services.Interfaces
{
    public interface IUserService
    {
        TokenResponse Authenticate (string username, string password);
        string RefreshToken(string token);
    }
}
