using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;

namespace Infra.Data.ModelConfiguration.Security
{
    public class UnidadeAcessoModelConfiguration : IEntityTypeConfiguration<UnidadeAcesso>
    {
        public void Configure(EntityTypeBuilder<UnidadeAcesso> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(UnidadeAcesso));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            entityTypeBuilder.HasData(
                new UnidadeAcesso()
                {
                    Id = new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490"),
                    Nome = "Unidade de Acesso para Testes",
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                });
        }
    }
}
