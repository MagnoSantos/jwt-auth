using jwtauth.Application.Dto;
using jwtauth.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using jwtauth.Infrastructure.CrossCutting;
using jwtauth.Domain.Entities;

namespace jwtauth.API.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEncodePasswordHelper _encodePasswordHelper;

        public UsuarioController(IUsuarioService usuarioService, IEncodePasswordHelper encodePasswordHelper)
        {
            _usuarioService = usuarioService;
            _encodePasswordHelper = encodePasswordHelper;
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

        [HttpGet]
        [Route("hash/gerar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EncodePassword(string senha)
        {
            return Ok(await _encodePasswordHelper.Encode(senha));
        }

        [HttpPost]
        [Route("hash/validar")]
        public async Task<IActionResult> ValidatePassword(string senha, EncodedPassword encodedPassword)
        {

            return Ok(await _encodePasswordHelper.Valid(senha, encodedPassword));
        }
    }
}