using jwtauth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace jwtauth.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> ObterPorId(Guid Id);

        Task Adicionar(TEntity entidade);
    }
}