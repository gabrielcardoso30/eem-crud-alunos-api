using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.ModelConfiguration.Security
{
    public class ParametroSistemaModelConfiguration : IEntityTypeConfiguration<ParametroSistema>
    {
        public void Configure(EntityTypeBuilder<ParametroSistema> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(ParametroSistema));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.ValorTexto)
                .HasMaxLength(200)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.AspNetUsers)
                .WithMany(p => p.ParametroSistema)
                .HasForeignKey(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
