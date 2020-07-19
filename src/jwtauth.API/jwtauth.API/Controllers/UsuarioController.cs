using jwtauth.Application.Dto;
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
    [Route("api/v1/usuario")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = await _usuarioService.Adicionar(usuarioDto);
            usuario.Senha = string.Empty;

            return Created(
                new Uri(Request.GetEncodedUrl()), 
                usuario
            );
        }
    }
}