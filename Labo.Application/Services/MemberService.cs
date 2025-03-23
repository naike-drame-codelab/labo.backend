using Labo.Application.DTO;
using Labo.Application.Exceptions;
using Labo.Application.Interfaces;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces.Services;
using Labo.Application.Utils;
using Labo.Domain.Entities;
using Labo.Domain.Enums;
using System.Transactions;

namespace Labo.Application.Services
{
    public class MemberService(
        IMemberRepository memberRepository, 
        IMailer mailer
    )
        : IMemberService
    {
        public bool ExistsEmail(string email)
        {
            return memberRepository.Any(m => m.Email == email);
        }

        public Member Register(RegisterMemberDTO dto)
        {
            // vérifier email unique
            if(memberRepository.Any(m => m.Email == dto.Email))
            {
                throw new DuplicatePropertyException(nameof(dto.Email));
            }
            // vérifier username unique
            if(memberRepository.Any(m => m.Username == dto.Username))
            {
                throw new DuplicatePropertyException(nameof(dto.Username));
            }
            // créer un mot de passe pour le membre
            string password = PasswordUtils.GeneratePassword();
            Guid salt = Guid.NewGuid();
            string hashedPassword = PasswordUtils.HashPassword(password, salt);

            using TransactionScope transactionScope = new ();
            // insérer le membre
            Member m = memberRepository.Add(new Member
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                BirthDate = dto.BirthDate,
                Elo = dto.Elo ?? 1200,
                Gender = dto.Gender,
                Role = Role.Member,
                Salt = salt
            });
            // envoyer un email à ce membre
            mailer.Send(m.Email, "Bienvenue sur le site Labo", $"Votre nom d'utilisateur est {m.Username} et votre mot de passe est {password}");
            transactionScope.Complete();

            return m;
        }
    }
}
