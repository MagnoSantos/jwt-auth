using jwtauth.Domain.Entities;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace jwtauth.Infrastructure.CrossCutting
{
    public class EncodePasswordHelper : IEncodePasswordHelper
    {
        private int _byteLength = 160 / 8;

        public Task<EncodedPassword> Encode(string senha, int iterations)
        {
            var populatedPassword = new EncodedPassword
            {
                Salt = CreateSalt(),
                Iterations = iterations
            };

            populatedPassword.Hash = CreateHash(senha, populatedPassword.Salt, iterations);
            return Task.FromResult(populatedPassword);
        }

        public Task<bool> Valid(string password, EncodedPassword encodedPassword)
        {
            var test = CreateHash(password, encodedPassword.Salt, encodedPassword.Iterations);

            return Task.FromResult(test == encodedPassword.Hash);
        }

        private byte[] CreateSalt()
        {
            var salt = new byte[_byteLength];

            using var saltGenerator = new RNGCryptoServiceProvider();
            saltGenerator.GetBytes(salt);

            return salt;
        }

        private byte[] CreateHash(string senha, byte[] salt, long iterations)
        {
            using var hashGenerator = new Rfc2898DeriveBytes(senha, salt, (int)iterations);
            return hashGenerator.GetBytes(_byteLength);
        }
    }
}