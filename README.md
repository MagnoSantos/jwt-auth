# jwt-auth
Autenticação JWT 

### Autenticação e Autorização com Bearer e JTW

- **Autenticação**: refere-se à verificação da identidade de uma entidade. 
- **Autorização**: trata do que uma entidade autenticada tem permissão para fazer. 

Para executar a autenticação através desse projeto será necessário um usuário, senha e perfil. Basta informar um usuário com perfil e ele gerará um token. Modelo de usuário para autenticação: 

```cs
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
```

Próximo item criado é o repositório, este está correlacionado a pasta Repositories como UserRepository.cs. Note que ele é implementado pela abstração *IBaseRepository* que disponibiliza os métodos CRUD. 

```cs
public class UsuarioRepository : IUsuarioRepository
{
   private readonly DataContext _dataContext;

   public UsuarioRepository(DataContext dataContext)
   {
      _dataContext = dataContext;
   }

   public async Task Adicionar(Usuario entidade)
   {
      _dataContext.Add(entidade);
      await _dataContext.SaveChangesAsync();
   }

   public async Task<Usuario> ObterPorId(Guid Id)
   {
      return await _dataContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => usuario.Id == Id);
   }
}
```

Está sendo utilizado o *ORM Entity Framework Core* e base de dados em memória, a sua configuração é realizada por intermédio do *DataContext*.

De forma complementar existe uma chave privada, para gerar o token. Esta chave corresponde a uma string que apenas o servidor conhece. 

Podemos gerar um Token simplesmente chamando o JwtSecurityTokenHandler.CreateToken e informando um SecurityTokenDescriptor, e embora isto possa ser feito dentro de um Controller, é interessante externalizado como ums serviço para reutilização. 

```cs
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
```

Adicionando o esquema de autenticação através do *AddAuthentication* informando ao ASP.NET que estamos utilizando uma autenticação no método ConfigureServices do Startup.cs

```cs
services.AddAuthentication(x =>
{	
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
```

Ao informar sobre a autenticação, já informamos também o tipo dela, neste caso JwtBearer e seu modelo de autenticação.

```cs
.AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
```

No método Configure:

```cs
app.UseAuthentication();
app.UseAuthorization();
```
