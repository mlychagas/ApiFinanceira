using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Dtos;
using ApiFinanceira.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiFinanceira.DataContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly AppDbContext _context;

        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        private Usuario usuario = new Usuario
        {
            Nome = "Joana",
            Email = "joana@gmail.com",
            Senha = "123456"
        };

        public AutenticacaoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {

            var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == user.Email);
            if (usuario == null)
            {
                return BadRequest("Email e/ou senha inválidos.");
            }
            var verificacaoSenha = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, user.Password);
            if (verificacaoSenha == PasswordVerificationResult.Failed)
            {
                return BadRequest("Senha inválida.");
            }
            var token = GerarJwtToken(usuario);

            return Ok(new { token });

        }

        private string GerarJwtToken(Usuario usuario)
        {
            var claims = new[]
            { 
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),  
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

// A função da classe AutenticacaoController é fornecer um endpoint de autenticação para os usuários.
// Ela recebe as credenciais do usuário (email e senha) através do endpoint "auth/login", verifica
// se as credenciais são válidas e, se forem, gera um token JWT (JSON Web Token) que pode
// ser usado para autenticar futuras requisições à API. O token contém informações sobre
// o usuário e tem um tempo de expiração definido.
