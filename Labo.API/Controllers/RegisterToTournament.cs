using Labo.Application.DTO;
using Labo.Application.Interfaces.Services;
using Labo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RegisterToTournament(IRegistrationService registrationService) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Post([FromBody] RegisterToTournamentDTO dto)
        {
            Tournament? t = registrationService.Register(dto.TournamentId, dto.MemberId);
            return Ok(t);
        }
    }
}
