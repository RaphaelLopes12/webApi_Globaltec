/*
* Catálodo do banco de dados que ficará armazenado em memória
*/
using Microsoft.EntityFrameworkCore;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Domain.Data {
    public class CatalogoDbContext : DbContext {
        // Cria o contexto do banco e monta a estrutura de dados
        public CatalogoDbContext(
            DbContextOptions<CatalogoDbContext> options) : base(options) { 
                //
            }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pessoa>().HasKey(p => p.CodigoPessoa);
        }
    }
}