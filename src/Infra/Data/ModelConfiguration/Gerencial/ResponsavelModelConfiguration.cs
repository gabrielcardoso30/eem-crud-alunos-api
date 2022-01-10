using Core.Entities.Gerencial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.ModelConfiguration.Gerencial
{

    public class ResponsavelModelConfiguration : IEntityTypeConfiguration<Responsavel>
    {

        public void Configure(EntityTypeBuilder<Responsavel> entityTypeBuilder)
        {

            entityTypeBuilder.ToTable(nameof(Responsavel));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Parentesco)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Email)
                //.IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.Aluno)
                .WithMany(p => p.Responsaveis)
                .HasForeignKey(d => d.AlunoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Responsaveis_Aluno_AlunoId");

        }

    }

}
