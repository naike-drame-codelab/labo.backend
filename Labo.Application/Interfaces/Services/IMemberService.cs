using Labo.Application.DTO;
using Labo.Domain.Entities;

namespace Labo.Application.Interfaces.Services
{
    public interface IMemberService
    {
        Member Register(RegisterMemberDTO dto);
        bool ExistsEmail(string email);
    }
}
