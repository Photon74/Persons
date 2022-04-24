namespace Persons.Services.Interfaces
{
    public interface IUserService
    {
        string Authenticate (string username, string password);
    }
}
