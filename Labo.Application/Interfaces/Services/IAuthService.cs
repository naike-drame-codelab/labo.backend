using Labo.Application.DTO;
using Labo.Domain.Entities;

namespace Labo.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Member? GetById(int id);
        public Member GetByUsernameOrEmail(LoginFormDTO dto);

    }
}