using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;

namespace Infra.Data.ModelConfiguration.Security
{
    public class GrupoAspNetUsersModelConfiguration : IEntityTypeConfiguration<GrupoAspNetUsers>
    {
        public void Configure(EntityTypeBuilder<GrupoAspNetUsers> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(GrupoAspNetUsers));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.HasOne(d => d.AspNetUsers)
                .WithMany(p => p.GrupoAspNetUsers)
                .HasForeignKey(d => d.AspNetUsersId);

            entityTypeBuilder.HasOne(d => d.Grupo)
                .WithMany(p => p.GrupoAspNetUsers)
                .HasForeignKey(d => d.GrupoId);

            entityTypeBuilder.HasData(
                new GrupoAspNetUsers()
                {
                    Id = new Guid("9b61b8f1-a5c0-4281-9976-4992e93e3c93"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    AspNetUsersId = new Guid("97E7E460-FC41-4924-AC91-C1AFE5813559"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                });
        }
    }
}
