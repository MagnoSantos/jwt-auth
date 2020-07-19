using jwtauth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace jwtauth.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}