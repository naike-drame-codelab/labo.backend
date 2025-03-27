using Labo.Application.DTO;
using Labo.Domain.Entities;

namespace Labo.Application.Interfaces.Services
{
    public interface IMemberService
    {
        Member Register(RegisterMemberDTO dto);
        List<Member> GetAll();

        Member GetById(int id);

        bool ExistsEmail(string email);
        void Delete(int id);

    }
}
