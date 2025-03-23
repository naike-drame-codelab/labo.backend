using Labo.Domain.Entities;
using Labo.Domain.Enums;

namespace Labo.Application.DTO
{
    public class RegisterMemberResultDTO(Member m)
    {
        public int Id { get; set; } = m.Id;
        public string Username { get; set; } = m.Username;
        public string Email { get; set; } = m.Email;
        public Gender Gender { get; set; } = m.Gender;
        public Role Role { get; set; } = m.Role;
        public DateTime BirthDate { get; set; } = m.BirthDate;
    }
}
