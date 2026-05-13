using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Dtos;
using ApiFinanceira.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ApiFinanceira.DataContexts;

namespace ApiFinanceira.Controllers
{
    [Route("/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Tornar AppDbContext público para resolver CS0051
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        // IDE0290: Usar construtor primário (C# 12+)
        public UsuarioController(AppDbContext contexto, IMapper mapper)
        {
            _context = contexto;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] UsuarioDto usuario)
        {
            try
            {
                var usuarioModel = _mapper.Map<Usuario>(usuario);
                usuarioModel.Senha = _passwordHasher.HashPassword(usuarioModel, usuario.Senha);
                await _context.Set<Usuario>().AddAsync(usuarioModel);
                await _context.SaveChangesAsync();
                return Ok(usuarioModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
