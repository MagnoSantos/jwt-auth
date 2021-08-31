using AutoFixture;
using jwtauth.Domain.Entities;
using jwtauth.Infrastructure.CrossCutting;
using System.Threading.Tasks;
using Xunit;

namespace jwtauth.Tests
{
    public class EncodePasswordHelperTests
    {
        private readonly IFixture _fixture;

        public EncodePasswordHelperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task EncodePassword_Sucesso()
        {
            var senha = "12345789";
            var encode = Instanciar();

            var retorno = await encode.Encode(senha);

            Assert.NotNull(retorno);
        }

        [Fact]
        public async Task EncodePasswordValidade_Sucesso()
        {
            var senha = "12345789";
            var encode = Instanciar();

            var retornoEncode = await encode.Encode(senha);
            var valid = await encode.Valid(senha, retornoEncode);

            Assert.True(valid);
        }

        private EncodePasswordHelper Instanciar()
        {
            return new EncodePasswordHelper();
        }
    }
}