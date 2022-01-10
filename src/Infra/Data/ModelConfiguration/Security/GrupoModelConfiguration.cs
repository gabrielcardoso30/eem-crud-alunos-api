using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;

namespace Infra.Data.ModelConfiguration.Security
{
    public class GrupoModelConfiguration : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(Grupo));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);

            entityTypeBuilder.HasData(
                new Grupo()
                {
                    Id = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    Nome = "Administrador",
                    Padrao = false,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                });
        }
    }
}
