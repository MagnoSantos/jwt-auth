using jwtauth.Application.Dto;
using jwtauth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace jwtauth.Domain.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Adicionar(UsuarioDto usuarioDto);

        Task<string> GerarToken(Guid id);
    }
}