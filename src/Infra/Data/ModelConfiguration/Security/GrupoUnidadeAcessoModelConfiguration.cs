using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;
using Core.Enums.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class GrupoUnidadeAcessoModelConfiguration : IEntityTypeConfiguration<GrupoUnidadeAcesso>
    {
        public void Configure(EntityTypeBuilder<GrupoUnidadeAcesso> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(GrupoUnidadeAcesso));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.HasOne(d => d.UnidadeAcesso)
                .WithMany(p => p.GrupoUnidadeAcesso)
                .HasForeignKey(d => d.UnidadeAcessoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrupoUnidadeAcessos_UnidadeAcesso_UnidadeAcessoId");

            entityTypeBuilder.HasData(
                new GrupoUnidadeAcesso()
                {
                    Id = new Guid("78cffbe4-d155-4b24-8abf-9df0f28aadf1"),
                    GrupoId= new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    UnidadeAcessoId = new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                }
            );
        }
    }
}
