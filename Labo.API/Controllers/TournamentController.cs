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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Tournament> list = tournamentService.GetAll();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                Tournament tournament = tournamentService.GetById(id);
                if (tournament == null)
                {
                    return NotFound();
                }
                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
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

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Tournament tournament = tournamentService.GetById(id);
                if (tournament == null)
                {
                    return NotFound();
                }
                tournamentService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
