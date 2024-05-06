using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Alunos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {

        // PROPRIEDADE PARA MANIPULAR A ENTIDADE DE REPRESENTANTE
        public DbSet<RepresentanteModel> Representantes { get; set; }

        // PROPRIEDADE PARA MANIPULAR A ENTIDADE DE CLIENTE
        public DbSet<ClienteModel> Clientes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                entity.ToTable("Representantes");
                entity.HasKey(e => e.RepresentanteId);
                entity.Property(e => e.NomeRepresentante).IsRequired();
                entity.HasIndex(e => e.Cpf).IsUnique(); 
            });

            modelBuilder.Entity<ClienteModel>(entity =>
            {
                // Define o nome da tabela para 'Clientes'
                entity.ToTable("Clientes"); 
                entity.HasKey(e => e.ClienteId); 
                entity.Property(e => e.Nome).IsRequired(); 
                entity.Property(e => e.Email).IsRequired();

                // Especifica o tipo de dado para DataNascimento
                entity.Property(e => e.DataNascimento).HasColumnType("date");
                entity.Property(e => e.Observacao).HasMaxLength(500);

                // Configuração da relação com RepresentanteModel
                // Define a relação de um para um com RepresentanteModel
                entity.HasOne(e => e.Representante)
                    // Indica que um Representante pode ter muitos Clientes
                    .WithMany()
                    // Define a chave estrangeira
                    .HasForeignKey(e => e.RepresentanteId)
                    // Torna a chave estrangeira obrigatória
                    .IsRequired(); 
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
