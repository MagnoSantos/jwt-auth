using jwtauth.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace jwtauth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Autenticar([FromBody] UsuarioDto usuarioDto)
        {
        }
    }
}