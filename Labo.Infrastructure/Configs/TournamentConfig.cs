using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Entities;
using Labo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labo.Infrastructure.Configs
{
    internal class TournamentConfig : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.Property(t => t.MinElo)
                .HasDefaultValue(0);
            builder.Property(t => t.MaxElo)
                .HasDefaultValue(3000);
            builder.Property(t => t.CurrentRound)
               .HasDefaultValue(0);
            builder.Property(t => t.WomenOnly)
               .HasDefaultValue(false);
            builder.Property(t => t.Status)
                .HasDefaultValue(Status.Pending);
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(t => t.LastUpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Tournament_MinPlayers", "MinPlayers BETWEEN 2 AND 32");
                t.HasCheckConstraint("CK_Tournament_MaxPlayers", "MaxPlayers BETWEEN 2 AND 32");
                t.HasCheckConstraint("CK_Tournament_MinPlayers_MaxPlayers", "MinPlayers <= MaxPlayers");
                t.HasCheckConstraint("CK_Tournament_MinElo", "MinElo >= 0 AND MinElo <= 3000");
                t.HasCheckConstraint("CK_Tournament_MaxElo", "MaxElo >= 0 AND MaxElo <= 3000");
                t.HasCheckConstraint("CK_Tournament_MinElo_MaxElo", "MinElo <= MaxElo");
                t.HasCheckConstraint("CK_Tournament_EndOfRegistrationDate", "EndOfRegistrationDate > DATEADD(day, MinPlayers, GETDATE())");
            });
        }
    }
}
