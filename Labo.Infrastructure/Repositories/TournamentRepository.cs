using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be.Khunly.EFRepository;
using Labo.Application.Interfaces.Repositories;
using Labo.Domain.Entities;

namespace Labo.Infrastructure.Repositories
{
    public class TournamentRepository(LaboContext context): RepositoryBase<Tournament>(context), ITournamentRepository
    {
    }
}
