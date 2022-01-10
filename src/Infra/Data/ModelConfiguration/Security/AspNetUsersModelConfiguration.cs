using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;

namespace Infra.Data.ModelConfiguration.Security
{

    public class AspNetUsersModelConfiguration : IEntityTypeConfiguration<AspNetUsers>
    {

        public void Configure(EntityTypeBuilder<AspNetUsers> entityTypeBuilder)
        {

            //entityTypeBuilder.ToTable(nameof(AspNetUsers));

            entityTypeBuilder.Property(e => e.Ativo)
                .HasDefaultValue(true)
                .IsRequired();

            entityTypeBuilder.HasData(
                new AspNetUsers()
                {
                    Id = new Guid("97E7E460-FC41-4924-AC91-C1AFE5813559"),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = null,
                    NormalizedEmail = null,
                    EmailConfirmed = false,
                    //!S3nh4123
                    PasswordHash = "AQAAAAEAACcQAAAAEPAZnzRwLn+hRi5+ehZKG33K7YXXEv+ftKZHbrV+smfDMypbHbVSqYVakJG+MUP3zA==",
                    SecurityStamp = "OXVDUJPW7QZIQ77MY2UR44NVKABXHWQZ",
                    ConcurrencyStamp = "5b4a83c8-9ea4-4689-ad5e-cc316bcfbed7",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    Nome = "Administrador",
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false,
                    TipoUsuario = 0,
                    QuantidadeLogin = 0,
                    PrimeiroLogin = false,
                    TermoUso = true,
                    CaminhoFoto = null,
                    TelefoneResidencial = null,
                    QuantidadePrimeiroAcesso = 0,
                    DataBloqueioPrimeiroAcesso = null,
                    UrlImagem = null,
                    CPF = null,
                    PlayerId = null,
                    Ativo = true
            });

        }

    }

}
