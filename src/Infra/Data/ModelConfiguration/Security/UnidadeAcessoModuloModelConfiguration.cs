using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;
using Core.Enums.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class UnidadeAcessoModuloModelConfiguration : IEntityTypeConfiguration<UnidadeAcessoModulo>
    {
        public void Configure(EntityTypeBuilder<UnidadeAcessoModulo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(UnidadeAcessoModulo));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.Property(e => e.Modulo)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.UnidadeAcesso)
                .WithMany(p => p.UnidadeAcessoModulo)
                .HasForeignKey(d => d.UnidadeAcessoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnidadeAcessoModulos_UnidadeAcesso_UnidadeAcessoId");

            entityTypeBuilder.HasData(
                new UnidadeAcessoModulo()
                {
                    Id = new Guid("0A78804C-FD66-4DCA-AB3E-439811E46B93"),
                    UnidadeAcessoId= new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490"),
                    Modulo = nameof(EnumModulo.Configuracao),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                }
            );
        }
    }
}
