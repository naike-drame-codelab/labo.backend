using Be.Khunly.EFRepository.Abstraction;
using Labo.Domain.Entities;

namespace Labo.Infrastructure.Repositories
{
    public interface ITournamentRepository : IRepositoryBase<Tournament>
    {
        Tournament? FindOneWithMembers(int id);
    }
}