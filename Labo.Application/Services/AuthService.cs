using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Application.DTO;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces.Services;
using Labo.Domain.Entities;

namespace Labo.Application.Services
{
    public class AuthService(IMemberRepository memberRepository) : IAuthService
    {
        public Member GetByUsernameOrEmail(LoginFormDTO dto)
        {
            memberRepository.FindOneWhere(m => m.Username == dto.UsernameOrEmail || m.Email == dto.UsernameOrEmail);
           return memberRepository.FindOneWhere(m => m.Username == dto.UsernameOrEmail || m.Email == dto.UsernameOrEmail) 
                ?? throw new KeyNotFoundException($"Email or username not found.");
        }

        public Member GetById(int id)
        {
            return memberRepository.FindOne(id) ?? throw new KeyNotFoundException($"Member with id {id} not found.");
        }
    }
}
