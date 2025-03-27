using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Entities;
using Labo.Domain.Enums;

namespace Labo.Application.DTO
{
    public record class LoginFormResultDTO(Member m)
    {
        public int Id { get; set; } = m.Id;
        public string Username { get; set; } = m.Username;
        public string Email { get; set; } = m.Email;
        public Role Role { get; set; } = m.Role;

    }
}
