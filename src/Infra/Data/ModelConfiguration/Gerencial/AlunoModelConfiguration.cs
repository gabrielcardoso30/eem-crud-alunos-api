using Core.Entities.Gerencial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.ModelConfiguration.Gerencial
{

    public class AlunoModelConfiguration : IEntityTypeConfiguration<Aluno>
    {

        public void Configure(EntityTypeBuilder<Aluno> entityTypeBuilder)
        {

            entityTypeBuilder.ToTable(nameof(Aluno));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Segmento)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.FotoUrl)
                //.IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Email)
                //.IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

        }

    }

}
