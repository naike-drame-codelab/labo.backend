using Labo.Domain.Enums;

namespace Labo.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid Salt { get; set; }
        public int Elo { get; set; }
        public DateTime BirthDate { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public List<Tournament>? Tournaments { get; set; }
    }
}
