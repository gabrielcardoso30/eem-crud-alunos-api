using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class AuditModelConfiguration : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(Auditoria));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.Property(e => e.Entidade)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.EntityState)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.KeyValue)
                .IsRequired()
                .HasMaxLength(4000)
                .IsUnicode(false);
                
            entityTypeBuilder.Property(e => e.ParentKeyValue)
                .HasMaxLength(4000)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.NewValues).IsUnicode(false);

            entityTypeBuilder.Property(e => e.OldValues).IsUnicode(false);
            
            entityTypeBuilder.HasIndex(e => e.AspNetUsersId);
            
            entityTypeBuilder.HasOne(ur => ur.AspNetUsers)
                .WithMany(u => u.Audit)
                .HasForeignKey(ur => ur.AspNetUsersId);
            
        }
    }
}
