using Microsoft.EntityFrameworkCore;
using Treino.Domain.Models;

namespace Treino.Repository.Context
{
    public class TreinoContext : DbContext
    {
        public TreinoContext(DbContextOptions<TreinoContext> options) : base(options)
        {
        }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Treinamento> Treinamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Treinamento>()
                .HasKey(t => t.Codigo);

            model.Entity<Empregado>()
                .HasKey(e => e.Matricula);

        }
    
    }
}