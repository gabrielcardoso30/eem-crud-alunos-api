using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;
using Core.Enums.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class GrupoModuloModelConfiguration : IEntityTypeConfiguration<GrupoModulo>
    {
        public void Configure(EntityTypeBuilder<GrupoModulo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(GrupoModulo));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.Property(e => e.Modulo)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.Grupo)
                .WithMany(p => p.GrupoModulo)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrupoModulos_Grupo_GrupoId");

            entityTypeBuilder.HasData(
                new GrupoModulo()
                {
                    Id = new Guid("78cffbe4-d155-4b24-8abf-9df0f28aadf1"),
                    GrupoId= new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    Modulo = nameof(EnumModulo.Configuracao),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                }
            );
        }
    }
}
