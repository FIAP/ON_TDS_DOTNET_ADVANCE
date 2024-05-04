using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Alunos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {

        // PROPRIEDADE PARA MANIPULAR A ENTIDADE DE REPRESENTANTE
        public DbSet<RepresentanteModel> Representantes { get; set; }


        // MÉTODO UTILIZADO PARA CRIAÇÃO DOS ELEMENTOS NO BANCO DE DADOS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                // Definindo um nome para tabela
                entity.ToTable("Representantes");

                // Definindo chave primária
                entity.HasKey(e => e.RepresentanteId);

                // Tornando o nome obrigatório
                entity.Property(e => e.NomeRepresentante).IsRequired();

                // Adicionando índice único para CPF
                entity.HasIndex(e => e.Cpf).IsUnique(); 
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}
