using jwtauth.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace jwtauth.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;


        public TokenService(IOptionsMonitor<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.CurrentValue;
        }

        public Task<string> Gerar(string login, string funcao)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_tokenOptions.ChavePrivada);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, funcao)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(
                tokenHandler.WriteToken(token)
            );
        }
    }
}