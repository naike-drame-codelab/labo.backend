using Labo.Domain.Enums;

namespace Labo.Application.Interfaces.Security
{
    public interface ITokenManager
    {
        string CreateToken(int id, string username, string email, Role role);
        int ValidateTokenWithoutLifeTime(string token);
    }
}
