using Labo.Domain.Entities;
using Labo.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace Labo.Infrastructure
{
    public class LaboContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfig());
        }
    }
}
