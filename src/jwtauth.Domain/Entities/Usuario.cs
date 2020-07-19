using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace jwtauth.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public Usuario(string login, string senha, string funcao)
        {
            Login = login;
            Senha = GerarHash(senha);
            Funcao = funcao;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }

        private string GerarHash(string senha)
        {
            var salt = new byte[16];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: senha, 
                    salt: salt, 
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 32
                )
            );
        }
    }
}