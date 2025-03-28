using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Entities;

namespace Labo.Application.Interfaces.Services
{
    public interface IRegistrationService
    {
        public Tournament? Register(int tournamentId, int memberId);
    }
}
