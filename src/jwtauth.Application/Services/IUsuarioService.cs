using System;
using System.Threading.Tasks;

namespace jwtauth.Domain.Services
{
    public interface IUsuarioService
    {
        Task<string> GerarToken(Guid id);
    }
}