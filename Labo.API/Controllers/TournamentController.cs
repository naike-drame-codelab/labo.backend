using System.Net.Mail;
using Labo.Application.DTO;
using Labo.Application.Exceptions;
using Labo.Application.Interfaces.Services;
using Labo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class TournamentController(ITournamentService tournamentService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CreateTournamentDTO dto)
        {
            try
            {
                //Member m = memberService.Register(dto);
                Tournament t = tournamentService.Create(dto);
                return Created("tournament/" + t.Id, new CreateTournamentResultDTO(t));
            }
            catch (DuplicatePropertyException ex)
            {
                return Conflict(ex.Message);
            }
            //catch (SmtpException)
            //{
            //    return Problem("L'email n'a pas pu être envoyé");
            //}
        }
    }
}
