using jwtauth.Application.Options;
using jwtauth.Application.Services;
using jwtauth.Domain.Repositories;
using jwtauth.Domain.Services;
using jwtauth.Infrastructure.Data.Context;
using jwtauth.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace jwtauth.Infrastructure.CrossCutting
{
    public static class IoCConfig
    {
        /// <summary>
        /// Injeta componentes da aplicação
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureComponents(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IEncodePasswordHelper, EncodePasswordHelper>();

            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("JwtAuth"));
            //Toda vez que for solicitado, cria inicialmente em memória e reutiliza. 
            services.AddScoped<DataContext>();
        }

        /// <summary>
        /// Injeta opções da aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureOptions(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<TokenOptions>(options =>
            {
                options.ChavePrivada = configuration.GetValue<string>("AppConfig:ChavePrivada");
            });
        }
    }
}