using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class AspNetUsersRefreshTokenModelConfiguration : IEntityTypeConfiguration<AspNetUsersRefreshToken>
    {
        public void Configure(EntityTypeBuilder<AspNetUsersRefreshToken> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(AspNetUsersRefreshToken));
            
            entityTypeBuilder.Property(e => e.ExpiredTime);

            entityTypeBuilder.Property(e => e.IssuedTime);

            entityTypeBuilder.Property(e => e.RefreshToken)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.AspNetUsers)
                .WithMany(p => p.AspNetUsersRefreshToken)
                .HasForeignKey(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
