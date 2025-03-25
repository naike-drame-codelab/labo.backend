using Labo.Application.DTO;
using Labo.Domain.Entities;

namespace Labo.Application.Interfaces.Services
{
    public interface ITournamentService
    {
        Tournament Create(CreateTournamentDTO dto);
        void Delete(int id);
        List<Tournament> GetAll();
        Tournament GetById(int id);
    }
}