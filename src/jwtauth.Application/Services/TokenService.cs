using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace jwtauth.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SecurityTokenDescriptor _tokenDescriptor;
        private readonly JwtSecurityTokenHandler _tokenHandler;


        public TokenService(
            SecurityTokenDescriptor tokenDescriptor,
            JwtSecurityTokenHandler tokenHandler
        )
        {
            _tokenDescriptor = tokenDescriptor;
            _tokenHandler = tokenHandler;
        }

        public Task<string> Gerar(string nome, string funcao)
        {
            
        }
    }
}