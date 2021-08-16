using jwtauth.Domain.Entities;
using System.Threading.Tasks;

namespace jwtauth.Infrastructure.CrossCutting
{
    public interface IEncodePasswordHelper
    {
        Task<EncodedPassword> Encode(string senha, int iterations);

        Task<bool> Valid(string password, EncodedPassword encodedPassword);
    }
}