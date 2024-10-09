using market.Domain;

namespace market.Repository;

public interface IAuthService
{
    Member? Login(string username, string password);
}