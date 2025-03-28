using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be.Khunly.EFRepository;
using Labo.Application.Interfaces.Repositories;
using Labo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labo.Infrastructure.Repositories
{
    public class TournamentRepository(LaboContext context): RepositoryBase<Tournament>(context), ITournamentRepository
    {
        public Tournament? FindOneWithMembers(int id)
        {
            return context.Tournaments.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
        }
    }
}
