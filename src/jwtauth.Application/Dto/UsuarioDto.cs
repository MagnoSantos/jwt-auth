using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace jwtauth.Application.Dto
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres")]
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [JsonPropertyName("funcao")]
        public string Funcao { get; set; }
    }
}