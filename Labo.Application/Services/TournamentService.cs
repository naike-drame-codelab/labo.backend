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
    public class TournamentService(ITournamentRepository tournamentRepository,
        IMailer mailer) : ITournamentService
    {
        public Tournament Create(CreateTournamentDTO dto)
        {
            if (tournamentRepository.Any(m => m.Name == dto.Name))
            {
                throw new DuplicatePropertyException(nameof(dto.Name));
            }

            using TransactionScope transactionScope = new ();
            // créer le tournoi
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
            // envoyer un email aux membres répondant aux conditions
            // créer class de check qui prendra un Member m et un Tournament t
            // foreach(Member m of eligibleMembers)
            // {
            //      mailer.Send(m.Email, $"Inscriptions ouvertes pour le tournoi {t.Name} ", $"Cher m.Username, vous remplissez les conditions pour participer au tournoi ! ");
            // };
            transactionScope.Complete();

            return t;
        }

    }
}
