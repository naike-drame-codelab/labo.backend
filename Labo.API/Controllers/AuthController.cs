using System.Security.Cryptography;
using System.Text;
using Labo.API.Security;
using Labo.Application.DTO;
using Labo.Application.Interfaces.Security;
using Labo.Application.Interfaces.Services;
using Labo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IAuthService authService, ITokenManager tokenManager) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormDTO dto)
        {
            try
            {
                Member? m = authService.GetByUsernameOrEmail(dto);
                if (m is null)
                {
                    return Unauthorized();
                }

                if (Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(dto.Password + m.Salt))) != m.Password)
                {
                    // 400 // 401
                    return BadRequest();
                }
                return Ok(new

                {
                    Token = tokenManager.CreateToken(m.Id, m.Username, m.Email, m.Role)
                }
                    );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

        }

        [HttpGet("refreshToken")]
        public IActionResult RefreshToken([FromQuery] string token)
        {
            try
            {
                int id = tokenManager.ValidateTokenWithoutLifeTime(token);
                //vérifier le user existe pour pouvoir passer ses données dans le nouveau token de rafraîchissement
                Member? m = authService.GetById(id);
                if (m is null)
                {
                    return Unauthorized();
                }
                string newToken = tokenManager.CreateToken(m.Id, m.Username, m.Email, m.Role);
                return Ok(new
                {
                    Token = newToken
                });
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }
    }
}
