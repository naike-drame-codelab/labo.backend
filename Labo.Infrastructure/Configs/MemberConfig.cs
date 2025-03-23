using Labo.Application.Utils;
using Labo.Domain.Entities;
using Labo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labo.Infrastructure.Configs
{
    internal class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(t => 
                t.HasCheckConstraint("CK_ELO", "Elo BETWEEN 0 AND 3000")
            );

            builder.Property(u => u.Username)
                .HasMaxLength(100);


            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Salt).IsUnique();

            builder.HasData([
                new Member {
                    Id = 1,
                    Username = "Checkmate",
                    Elo = 1500,
                    Email = "lykhun@gmail.com",
                    Gender = Gender.Male,
                    Role = Role.Admin,
                    Salt = new Guid(),
                    BirthDate = new DateTime(1982,5,6),
                    Password = PasswordUtils.HashPassword("1234", new Guid())
                }
            ]);
        }
    }
}
