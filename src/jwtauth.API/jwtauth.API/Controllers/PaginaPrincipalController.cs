using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace jwtauth.API.Controllers
{
    [ApiController]
    [Route("api/v1/paginaprincipal")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PaginaPrincipalController : ControllerBase
    {
        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Authenticated()
        {
            return string.Format("Autenticado - {0}", this.User.Identity.Name);
        }

        [HttpGet]
        [Route("usuariofinal")]
        [Authorize(Roles = "usuariofinal")]
        public string UsuarioFinal()
        {
            return "Usuário Final";
        }
    }
}