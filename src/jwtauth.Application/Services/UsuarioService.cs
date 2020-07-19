using jwtauth.Domain.Repositories;
using jwtauth.Domain.Services;
using System;
using System.Threading.Tasks;

namespace jwtauth.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _authService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            ITokenService authService
        )
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<string> GerarToken(Guid id)
        {
            var usuarioObtido = await _usuarioRepository.ObterPorId(id);

            if (usuarioObtido == null)
            {
                return null;
            }


        }
    }
}