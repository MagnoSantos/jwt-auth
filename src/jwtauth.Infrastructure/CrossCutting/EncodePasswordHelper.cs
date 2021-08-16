using jwtauth.Domain.Entities;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace jwtauth.Infrastructure.CrossCutting
{
    public class EncodePasswordHelper : IEncodePasswordHelper
    {
        private readonly int _byteLength = 160 / 8;
        private readonly int _iterations = 1000;

        public Task<EncodedPassword> Encode(string senha)
        {
            var populatedPassword = new EncodedPassword
            {
                Salt = CreateSalt(),
            };

            populatedPassword.Hash = CreateHash(senha, populatedPassword.Salt);
            return Task.FromResult(populatedPassword);
        }

        public Task<bool> Valid(string password, EncodedPassword encodedPassword)
        {
            var testHash = CreateHash(password, encodedPassword.Salt);

            return Task.FromResult(testHash.SequenceEqual(encodedPassword.Hash));
        }

        private byte[] CreateSalt()
        {
            var salt = new byte[_byteLength];

            using var saltGenerator = new RNGCryptoServiceProvider();
            saltGenerator.GetBytes(salt);

            return salt;
        }

        private byte[] CreateHash(string senha, byte[] salt)
        {
            using var hashGenerator = new Rfc2898DeriveBytes(senha, salt, _iterations);
            return hashGenerator.GetBytes(_byteLength);
        }
    }
}