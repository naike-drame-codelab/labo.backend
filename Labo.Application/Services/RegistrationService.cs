using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces.Services;
using Labo.Application.Utils;
using Labo.Domain.Entities;
using Labo.Domain.Enums;
using Labo.Infrastructure.Repositories;

namespace Labo.Application.Services
{
    public class RegistrationService(ITournamentRepository tournamentRepository, IMemberRepository memberRepository) : IRegistrationService
    {
        public Tournament? Register(int tournamentId, int memberId)
        {
            Tournament? t = tournamentRepository.FindOneWithMembers(tournamentId);
            Member? m = memberRepository.FindOne(memberId);

            if (t == null || m == null)
            {
                throw new KeyNotFoundException("Invalid tournament or member.");
            }

            if (t.Status != Status.Pending)
            {
                throw new Exception("The tournament is not open for registration.");
            }

            if (t.EndOfRegistrationDate < DateTime.UtcNow)
            {
                throw new Exception("The registration period has ended.");
            }

            if (t.Players.Count > 0)
            {
                if (t.Players.Any(p => p.Id == memberId))
                {
                    throw new Exception("The member is already registered.");
                }
            }

            if (t.Players.Count >= t.MaxPlayers)
            {
                throw new Exception("The tournament has reached the maximum number of players.");
            }

            if ((t.MinElo.HasValue && m.Elo < t.MinElo) || (t.MaxElo.HasValue && m.Elo > t.MaxElo))
            {
                throw new Exception("The member's ELO does not meet the tournament's requirements.");
            }

            if (t.WomenOnly && (m.Gender != Gender.Female || m.Gender != Gender.Other))
            {
                throw new Exception("This tournament is restricted to women or others.");
            }

            int ageAtEndOfRegistration = RegisterToTournamentUtils.CalculateAge(m.BirthDate, t.EndOfRegistrationDate);
            if (!RegisterToTournamentUtils.IsEligible(ageAtEndOfRegistration, t.Categories))
                throw new Exception("The member's age does not meet the tournament's requirements.");

            t.Players.Add(m);

            return tournamentRepository.Update(t);
        }
    }
}
