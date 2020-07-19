using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace jwtauth.API.Controllers
{
    [ApiController]
    [Route("api/v1/home")]
    [Produces(MediaTypeNames.Application.Json)]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Obtem a autenticação e apresenta o nome do autor da solicitação.
        /// </summary>
        /// <response code="401">Retorna que o autor da requisição não está autorizado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public string Autenticado() => string.Format("Usuario Autenticado - {0}", User.Identity.Name);

        /// <summary>
        /// Obtem a autenticação com base no nível de acesso - usuariofinal
        /// </summary>
        /// <response code="401">Retorna que o autor da requisição não está autorizado</response>
        /// <response code="403">Identificado o autor da requsição, porém é recusado a autorização pelo nível de acesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("usuariofinal")]
        [Authorize(Roles = "usuariofinal")]
        public string UsuarioFinal() => "Nível de acesso: Usuário Final";

        /// <summary>
        /// Obtem a autenticação com base no nível de acesso - admin
        /// </summary>
        /// <response code="401">Retorna que o autor da requisição não está autorizado</response>
        /// <response code="403">Identificado o autor da requsição, porém é recusado a autorização pelo nível de acesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Administrador() => "Nível de acesso: Administrador";
    }
}