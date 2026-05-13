using ApiFinanceira.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Despesa> Despesas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Tag> Tags{get; set; }
        public object Usuarios { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>()
                .HasMany(d => d.Tags)
                .WithMany(t => t.Despesas)
                .UsingEntity <Dictionary<string, object>>( "DespesaTag",
                    f => f.HasOne<Tag>().WithMany().HasForeignKey("tag_id"),
                    f => f.HasOne<Despesa>().WithMany().HasForeignKey("despesa_id"),
                    f => f.ToTable("despesa_tags")
                );
        }
    }
}

// quando se cria um model e esse representa uma entidade
// ele deve ser configurado aqui para funcionar
