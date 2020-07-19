using jwtauth.Application.Dto;
using jwtauth.Domain.Entities;
using jwtauth.Domain.Repositories;
using jwtauth.Domain.Services;
using System;
using System.Threading.Tasks;

namespace jwtauth.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService
        )
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<Usuario> Adicionar(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario(
                login: usuarioDto.Login,
                senha: usuarioDto.Senha,
                funcao: usuarioDto.Funcao
            );

            await _usuarioRepository.Adicionar(usuario);

            return usuario;
        }

        public async Task<string> GerarToken(Guid id)
        {
            var usuarioObtido = await _usuarioRepository.ObterPorId(id);

            if (usuarioObtido == null)
                return null;

            return await _tokenService.Gerar(usuarioObtido.Login, usuarioObtido.Funcao);
        }
    }
}