using ApiFinanceira.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.DataContexts
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options) { }

        public DbSet<Despesa> Despesas { get; set; }
    }
}
