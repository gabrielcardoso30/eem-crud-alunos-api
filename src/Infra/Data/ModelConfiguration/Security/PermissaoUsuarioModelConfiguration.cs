using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;

namespace Infra.Data.ModelConfiguration.Security
{
    public class PermissaoUsuarioModelConfiguration : IEntityTypeConfiguration<PermissaoUsuario>
    {
        public void Configure(EntityTypeBuilder<PermissaoUsuario> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(PermissaoUsuario));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.HasOne(d => d.AspNetUsers)
                .WithMany(p => p.PermissaoUsuario)
                .HasForeignKey(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.Permissao)
                .WithMany(p => p.PermissaoUsuario)
                .HasForeignKey(d => d.PermissaoId);

            ////PERMISSÃO: CONSULTAR USUÁRIO
            //entityTypeBuilder.HasData(
            //    new PermissaoUsuario()
            //    {
            //        Id = Guid.NewGuid(),
            //        AspNetUsersId = new Guid("97E7E460-FC41-4924-AC91-C1AFE5813559"),
            //        DataCriacao = DateTime.Now,
            //        PermissaoId = new Guid("460868E7-6B2B-4D8D-A56C-C096E236DDCD"),                    
            //    });

        }
    }
}
