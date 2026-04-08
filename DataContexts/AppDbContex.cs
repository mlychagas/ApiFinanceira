using ApiFinanceira.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.DataContexts
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options) { }

        public DbSet<Despesa> Despesas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}

// quando se cria um model e esse representa uma entidade
// ele deve ser configurado aqui para funcionar
