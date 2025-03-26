using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces;
using Labo.Infrastructure.Repositories;
using Labo.Domain.Entities;
using Labo.Application.Exceptions;
using Labo.Application.DTO;
using Labo.Application.Interfaces.Services;
using System.Transactions;

namespace Labo.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository tournamentRepository;
        private readonly IMailer mailer;

        public TournamentService(ITournamentRepository tournamentRepository, IMailer mailer)
        {
            this.tournamentRepository = tournamentRepository;
            this.mailer = mailer;
        }

        public Tournament Create(CreateTournamentDTO dto)
        {
            if (tournamentRepository.Any(m => m.Name == dto.Name))
            {
                throw new DuplicatePropertyException(nameof(dto.Name));
            }

            using TransactionScope transactionScope = new();
            Tournament t = tournamentRepository.Add(new Tournament
            {
                Name = dto.Name,
                Location = dto.Location,
                MinPlayers = dto.MinPlayers,
                MaxPlayers = dto.MaxPlayers,
                MinElo = dto.MinElo,
                MaxElo = dto.MaxElo,
                Categories = dto.Categories,
                EndOfRegistrationDate = dto.EndOfRegistrationDate,
                WomenOnly = dto.WomenOnly,
            });

            transactionScope.Complete();
            return t;
        }

        public void Delete(int id)
        {
            Tournament? t = tournamentRepository.FindOne(id);
            if (t == null)
            {
                throw new KeyNotFoundException($"Tournament with id {id} not found.");
            }

            tournamentRepository.Remove(t);
        }

        public List<Tournament> GetAll() => tournamentRepository.Find();

        public Tournament GetById(int id)
        {
            return tournamentRepository.FindOne(id) ?? throw new KeyNotFoundException($"Tournament with id {id} not found.");
        }
    }
}
