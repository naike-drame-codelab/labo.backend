using Labo.Application.Validators;
using Labo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Labo.Application.DTO
{
    public class RegisterMemberDTO
    {
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [MaxLength(400)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [BeforeToday]
        public DateTime BirthDate { get; set; }

        [Range(0, 3000)]
        public int? Elo { get; set; }

        public Gender Gender { get; set; }
    }
}
