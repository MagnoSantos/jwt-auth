using jwtauth.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace jwtauth.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Autenticar(Guid id)
        {
            var token = await _usuarioService.GerarToken(id);

            if (token == null)
                return NotFound("Usuario não encontrado");

            return Created(
                new Uri(Request.GetEncodedUrl()),
                new { token }
            );
        }
    }
}