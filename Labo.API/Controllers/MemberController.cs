using Labo.Application.DTO;
using Labo.Application.Exceptions;
using Labo.Application.Interfaces.Services;
using Labo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMemberService memberService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] RegisterMemberDTO dto)
        {
            try
            {
                Member m = memberService.Register(dto);
                return Created("member/" + m.Id, new RegisterMemberResultDTO(m));
            }
            catch (DuplicatePropertyException ex)
            {
                return Conflict(ex.Message);
            }
            catch (SmtpException)
            {
                return Problem("L'email n'a pas pu être envoyé");
            }
        }

        [HttpHead]
        public IActionResult Head([FromQuery]string email)
        {
            if(memberService.ExistsEmail(email))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
