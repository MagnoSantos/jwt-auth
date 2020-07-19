using jwtauth.Domain.Entities;
using jwtauth.Domain.Repositories;
using jwtauth.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace jwtauth.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _dataContext;

        public UsuarioRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Adicionar(Usuario entidade)
        {
            _dataContext.Add(entidade);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Usuario> ObterPorId(Guid Id)
        {
            return await _dataContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => usuario.Id == Id);
        }
    }
}