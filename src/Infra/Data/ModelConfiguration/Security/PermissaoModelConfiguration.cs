using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;

namespace Infra.Data.ModelConfiguration.Security
{
    public class PermissaoModelConfiguration : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(Permissao));

            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);
            
            entityTypeBuilder.Property(e => e.Descricao)
                .IsUnicode(false);

            entityTypeBuilder.HasData(

                //UNIDADES DE ACESSO
                new Permissao()
                {
                    Id = new Guid("E1F523A6-0217-44A8-A649-A0D1B22505C3"),
                    Nome = "ConsultarUnidadeAcesso",
                    Descricao = "Consultar Unidade de Acesso",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("B3EA5DCD-B6CF-48D5-90A3-6A0972CC72AD"),
                    Nome = "CadastrarUnidadeAcesso",
                    Descricao = "Cadastrar Unidade de Acesso",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("8490DC09-50CD-42D4-87C6-B70EC7E488D5"),
                    Nome = "AtualizarUnidadeAcesso",
                    Descricao = "Atualizar Unidade de Acesso",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("71F80EEF-D10E-41D2-A803-E5B0F563B0DA"),
                    Nome = "DeletarUnidadeAcesso",
                    Descricao = "Deletar Unidade de Acesso",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },

                new Permissao()
                {
                    Id = new Guid("460868E7-6B2B-4D8D-A56C-C096E236DDCD"),
                    Nome = "ConsultarUsuario",
                    Descricao = "Consultar Usuario",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("EC7D9981-5E78-4B3E-95F4-27958C0D4F66"),
                    Nome = "CadastrarUsuario",
                    Descricao = "Cadastrar Usuario",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("9382B87B-1401-4B51-BDFA-5BBACA41CB78"),
                    Nome = "AtualizarUsuario",
                    Descricao = "Atualizar Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("FD1324D8-5C31-4FDB-B629-B9755B3DC9EF"),
                    Nome = "DeletarUsuario",
                    Descricao = "Deletar Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("D4A39D1C-C7FD-41DE-AA56-CF269F723002"),
                    Nome = "ResetarSenhaUsuario",
                    Descricao = "Resetar Senha do Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("7298E2AE-C46A-47FF-9AE5-D9620E0B288C"),
                    Nome = "DesbloquearUsuario",
                    Descricao = "Desbloquear Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                // =================>
                new Permissao()
                {
                    Id = new Guid("68943F93-71AF-4368-A57A-9F0B2D77F56E"),
                    Nome = "AssociarUsuarioGrupo",
                    Descricao = "Associar Usuário a um Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                // =================>
                new Permissao()
                {
                    Id = new Guid("59666E7E-320F-44E2-9D14-97A131A78618"),
                    Nome = "ConsultarGrupo",
                    Descricao = "Consultar Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("1E7D4555-BE2F-421F-9FEE-7C22F4326B60"),
                    Nome = "CadastrarGrupo",
                    Descricao = "Cadastrar Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("F9114F1D-D1A0-4AFA-B1BC-B8137A5B7640"),
                    Nome = "AtualizarGrupo",
                    Descricao = "Atualizar Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("ED2B7A70-90AE-44D4-B81E-506EF914C34C"),
                    Nome = "DeletarGrupo",
                    Descricao = "Deletar Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                // =================>
                new Permissao()
                {
                    Id = new Guid("684FD917-CA64-47A2-B5EA-5CF801A36A93"),
                    Nome = "ConsultarPermissoesGrupo",
                    Descricao = "Consultar Permissões de Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("36B7322E-42A9-4828-9D84-D0DBFAC3E27D"),
                    Nome = "CadastrarPermissaoGrupo",
                    Descricao = "Cadastrar Permissão de Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("4B31BFDB-790B-408E-B8D6-60397CB489C3"),
                    Nome = "DeletarPermissaoGrupo",
                    Descricao = "Deletar Permissão de Grupo",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                // =================>
                new Permissao()
                {
                    Id = new Guid("9BB40F14-1EA7-4C84-A3A8-48CFFB7E921F"),
                    Nome = "ConsultarPermissoesUsuario",
                    Descricao = "Consultar Permissões de Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("1A31DFCF-234B-4745-9C3B-2F41BBD3021D"),
                    Nome = "CadastrarPermissaoUsuario",
                    Descricao = "Cadastrar Permissão de Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                },
                new Permissao()
                {
                    Id = new Guid("1E8731B6-338D-4F3F-9EBB-045A2B18156A"),
                    Nome = "DeletarPermissaoUsuario",
                    Descricao = "Deletar Permissão de Usuário",
                    TipoUsuario = 0,
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local),
                    Deletado = false
                }

                

            );
        }
    }
}
