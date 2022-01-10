using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Padrao = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Descricao = table.Column<string>(unicode: false, nullable: true),
                    TipoUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeAcesso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeAcesso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    TipoUsuario = table.Column<int>(nullable: false),
                    QuantidadeLogin = table.Column<int>(nullable: false),
                    PrimeiroLogin = table.Column<bool>(nullable: false),
                    TermoUso = table.Column<bool>(nullable: false),
                    CaminhoFoto = table.Column<string>(nullable: true),
                    TelefoneResidencial = table.Column<string>(nullable: true),
                    QuantidadePrimeiroAcesso = table.Column<int>(nullable: false),
                    DataBloqueioPrimeiroAcesso = table.Column<DateTime>(nullable: true),
                    UrlImagem = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    PlayerId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false, defaultValue: true),
                    UnidadeAcessoSelecionada = table.Column<Guid>(nullable: true),
                    GrupoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoModulo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false),
                    Modulo = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoModulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoModulos_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoGrupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false),
                    PermissaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissaoGrupo_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissaoGrupos_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUnidadeAcesso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUnidadeAcesso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoUnidadeAcessos_UnidadeAcesso_UnidadeAcessoId",
                        column: x => x.UnidadeAcessoId,
                        principalTable: "UnidadeAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeAcessoModulo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    Modulo = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeAcessoModulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeAcessoModulos_UnidadeAcesso_UnidadeAcessoId",
                        column: x => x.UnidadeAcessoId,
                        principalTable: "UnidadeAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsersRefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUsersId = table.Column<Guid>(nullable: false),
                    RefreshToken = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    IssuedTime = table.Column<DateTime>(nullable: false),
                    ExpiredTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsersRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsersRefreshToken_AspNetUsers_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Entidade = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    ParentKeyValue = table.Column<string>(unicode: false, maxLength: 4000, nullable: true),
                    KeyValue = table.Column<string>(unicode: false, maxLength: 4000, nullable: false),
                    OldValues = table.Column<string>(unicode: false, nullable: true),
                    NewValues = table.Column<string>(unicode: false, nullable: true),
                    EntityState = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    AspNetUsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auditoria_AspNetUsers_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoAspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    AspNetUsersId = table.Column<Guid>(nullable: false),
                    GrupoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoAspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoAspNetUsers_AspNetUsers_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoAspNetUsers_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParametroSistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    TipoParametro = table.Column<int>(nullable: true),
                    TipoValor = table.Column<int>(nullable: true),
                    ValorBit = table.Column<bool>(nullable: true),
                    ValorTexto = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    AspNetUsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametroSistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametroSistema_AspNetUsers_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    AspNetUsersId = table.Column<Guid>(nullable: false),
                    PermissaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuario_AspNetUsers_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuario_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Ativo", "CPF", "CaminhoFoto", "ConcurrencyStamp", "DataBloqueioPrimeiroAcesso", "DataCriacao", "DataUltimaAtualizacao", "Deletado", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PlayerId", "PrimeiroLogin", "QuantidadeLogin", "QuantidadePrimeiroAcesso", "SecurityStamp", "TelefoneResidencial", "TermoUso", "TipoUsuario", "TwoFactorEnabled", "UnidadeAcessoSelecionada", "UrlImagem", "UserName", "UsuarioIdUltimaAtualizacao", "GrupoId" },
                values: new object[] { new Guid("97e7e460-fc41-4924-ac91-c1afe5813559"), 0, true, null, null, "5b4a83c8-9ea4-4689-ad5e-cc316bcfbed7", null, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "AspNetUsers", null, false, true, null, "Administrador", null, "ADMIN", "AQAAAAEAACcQAAAAEPAZnzRwLn+hRi5+ehZKG33K7YXXEv+ftKZHbrV+smfDMypbHbVSqYVakJG+MUP3zA==", null, false, null, false, 0, 0, "OXVDUJPW7QZIQ77MY2UR44NVKABXHWQZ", null, true, 0, false, null, null, "admin", null, null });

            migrationBuilder.InsertData(
                table: "Grupo",
                columns: new[] { "Id", "Ativo", "DataCriacao", "DataUltimaAtualizacao", "Deletado", "Nome", "Padrao", "UnidadeAcessoId", "UsuarioIdUltimaAtualizacao" },
                values: new object[] { new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Administrador", false, new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.InsertData(
                table: "Permissao",
                columns: new[] { "Id", "Ativo", "DataCriacao", "DataUltimaAtualizacao", "Deletado", "Descricao", "Nome", "TipoUsuario", "UnidadeAcessoId", "UsuarioIdUltimaAtualizacao" },
                values: new object[,]
                {
                    { new Guid("c11831f3-752d-44ce-a6ce-8c2e5fb177e6"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Atualizar Aluno", "AtualizarAluno", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("21fb4244-6039-459a-9f34-f491564f76d5"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Aluno", "CadastrarAluno", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("2319d23c-9f81-42d1-8529-37a91a307ea7"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Aluno", "ConsultarAluno", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("757c0627-9e30-40b5-85b8-04935c037d40"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Responsável", "DeletarResponsavel", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("af411c34-84ca-4fe1-9a9a-c09677f75d38"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Atualizar Responsável", "AtualizarResponsavel", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("d13364f3-bac9-424e-9f8c-e5c816c71db9"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Responsável", "CadastrarResponsavel", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("f5c0ded7-355f-4026-9141-ad97e6f8ba5e"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Responsável", "ConsultarResponsavel", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("1e8731b6-338d-4f3f-9ebb-045a2b18156a"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Permissão de Usuário", "DeletarPermissaoUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("1a31dfcf-234b-4745-9c3b-2f41bbd3021d"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Permissão de Usuário", "CadastrarPermissaoUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("9bb40f14-1ea7-4c84-a3a8-48cffb7e921f"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Permissões de Usuário", "ConsultarPermissoesUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("4b31bfdb-790b-408e-b8d6-60397cb489c3"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Permissão de Grupo", "DeletarPermissaoGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("36b7322e-42a9-4828-9d84-d0dbfac3e27d"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Permissão de Grupo", "CadastrarPermissaoGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("684fd917-ca64-47a2-b5ea-5cf801a36a93"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Permissões de Grupo", "ConsultarPermissoesGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("ed2b7a70-90ae-44d4-b81e-506ef914c34c"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Grupo", "DeletarGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("f9114f1d-d1a0-4afa-b1bc-b8137a5b7640"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Atualizar Grupo", "AtualizarGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("1e7d4555-be2f-421f-9fee-7c22f4326b60"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Grupo", "CadastrarGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("59666e7e-320f-44e2-9d14-97a131a78618"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Grupo", "ConsultarGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("68943f93-71af-4368-a57a-9f0b2d77f56e"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Associar Usuário a um Grupo", "AssociarUsuarioGrupo", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("7298e2ae-c46a-47ff-9ae5-d9620e0b288c"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Desbloquear Usuário", "DesbloquearUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("d4a39d1c-c7fd-41de-aa56-cf269f723002"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Resetar Senha do Usuário", "ResetarSenhaUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("fd1324d8-5c31-4fdb-b629-b9755b3dc9ef"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Usuário", "DeletarUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("9382b87b-1401-4b51-bdfa-5bbaca41cb78"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Atualizar Usuário", "AtualizarUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("ec7d9981-5e78-4b3e-95f4-27958c0d4f66"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Usuario", "CadastrarUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("460868e7-6b2b-4d8d-a56c-c096e236ddcd"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Usuario", "ConsultarUsuario", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("71f80eef-d10e-41d2-a803-e5b0f563b0da"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Unidade de Acesso", "DeletarUnidadeAcesso", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("8490dc09-50cd-42d4-87c6-b70ec7e488d5"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Atualizar Unidade de Acesso", "AtualizarUnidadeAcesso", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("b3ea5dcd-b6cf-48d5-90a3-6a0972cc72ad"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Cadastrar Unidade de Acesso", "CadastrarUnidadeAcesso", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("e1f523a6-0217-44a8-a649-a0d1b22505c3"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Consultar Unidade de Acesso", "ConsultarUnidadeAcesso", 0, new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("b43414d1-7e22-4636-98a3-7db191b509c4"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Deletar Aluno", "DeletarAluno", 0, new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                table: "UnidadeAcesso",
                columns: new[] { "Id", "Ativo", "DataCriacao", "DataUltimaAtualizacao", "Deletado", "Nome", "UsuarioIdUltimaAtualizacao" },
                values: new object[] { new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, "Unidade de Acesso para Testes", null });

            migrationBuilder.InsertData(
                table: "GrupoAspNetUsers",
                columns: new[] { "Id", "AspNetUsersId", "Ativo", "DataCriacao", "DataUltimaAtualizacao", "Deletado", "GrupoId", "UnidadeAcessoId", "UsuarioIdUltimaAtualizacao" },
                values: new object[] { new Guid("9b61b8f1-a5c0-4281-9976-4992e93e3c93"), new Guid("97e7e460-fc41-4924-ac91-c1afe5813559"), true, new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), null, false, new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.InsertData(
                table: "GrupoModulo",
                columns: new[] { "Id", "DataCriacao", "GrupoId", "Modulo" },
                values: new object[] { new Guid("78cffbe4-d155-4b24-8abf-9df0f28aadf1"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), "Configuracao" });

            migrationBuilder.InsertData(
                table: "GrupoUnidadeAcesso",
                columns: new[] { "Id", "DataCriacao", "GrupoId", "UnidadeAcessoId" },
                values: new object[] { new Guid("78cffbe4-d155-4b24-8abf-9df0f28aadf1"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490") });

            migrationBuilder.InsertData(
                table: "PermissaoGrupo",
                columns: new[] { "Id", "DataCriacao", "GrupoId", "PermissaoId" },
                values: new object[,]
                {
                    { new Guid("d766e2e4-c0e1-46a6-9185-32356c616dfd"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("c11831f3-752d-44ce-a6ce-8c2e5fb177e6") },
                    { new Guid("75c2f6b8-a2ae-4f62-af7d-1acd93ed5711"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("21fb4244-6039-459a-9f34-f491564f76d5") },
                    { new Guid("8559a05d-afe3-432c-8d5d-00ead9b318eb"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("2319d23c-9f81-42d1-8529-37a91a307ea7") },
                    { new Guid("5da15091-299e-49b2-9c5a-6dcb1fae7376"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("757c0627-9e30-40b5-85b8-04935c037d40") },
                    { new Guid("9bf81381-6ff8-4e89-b636-a607684960bc"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("af411c34-84ca-4fe1-9a9a-c09677f75d38") },
                    { new Guid("9d2dce8c-b79a-41d5-9df9-a3a8c0c7b060"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("684fd917-ca64-47a2-b5ea-5cf801a36a93") },
                    { new Guid("6debb8af-f32c-4fa1-899b-ef26f2401fbd"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("f5c0ded7-355f-4026-9141-ad97e6f8ba5e") },
                    { new Guid("5bf94d8e-947a-43b3-8f7c-eecaed54fd54"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("1e8731b6-338d-4f3f-9ebb-045a2b18156a") },
                    { new Guid("5b4b6ded-2693-4a14-b08b-0ed3c3b8f164"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("1a31dfcf-234b-4745-9c3b-2f41bbd3021d") },
                    { new Guid("1ff30662-6cb9-4971-ad7d-883264b939a6"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("9bb40f14-1ea7-4c84-a3a8-48cffb7e921f") },
                    { new Guid("1982b23d-6602-4ef9-b55d-6607aa83e5e5"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("4b31bfdb-790b-408e-b8d6-60397cb489c3") },
                    { new Guid("25a377c4-7ec7-48f6-8973-a5fe5bfe5fe7"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("36b7322e-42a9-4828-9d84-d0dbfac3e27d") },
                    { new Guid("6b86ef2d-edad-4784-8fb5-da69348d5737"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("b43414d1-7e22-4636-98a3-7db191b509c4") },
                    { new Guid("4048f7f8-897e-400d-9615-ddda89c8c2d4"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("d13364f3-bac9-424e-9f8c-e5c816c71db9") },
                    { new Guid("42c698ee-8eb5-4f9e-b13b-e7b4812c0320"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("ed2b7a70-90ae-44d4-b81e-506ef914c34c") },
                    { new Guid("81289871-b778-4e86-a009-2bca85468124"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("1e7d4555-be2f-421f-9fee-7c22f4326b60") },
                    { new Guid("96042d19-e425-4096-86d1-6a816b0a389d"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("59666e7e-320f-44e2-9d14-97a131a78618") },
                    { new Guid("407d89aa-6ee5-43a5-b868-66319b28d48a"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("68943f93-71af-4368-a57a-9f0b2d77f56e") },
                    { new Guid("45a1a818-4d5d-4f80-8e7c-9957746bda46"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("7298e2ae-c46a-47ff-9ae5-d9620e0b288c") },
                    { new Guid("1a084f91-86d1-4dfc-9a8e-04043db99e83"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("d4a39d1c-c7fd-41de-aa56-cf269f723002") },
                    { new Guid("b4a4642f-c59d-4ba4-8b85-339ef9a1d784"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("fd1324d8-5c31-4fdb-b629-b9755b3dc9ef") },
                    { new Guid("ebb1ceed-cdbf-4081-80ad-bdbd30f5875f"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("9382b87b-1401-4b51-bdfa-5bbaca41cb78") },
                    { new Guid("25ae9b26-d36f-4eb6-870a-4ad5272f3821"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("ec7d9981-5e78-4b3e-95f4-27958c0d4f66") },
                    { new Guid("fffa2477-10a8-4955-9c4a-4b0251210b9d"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("460868e7-6b2b-4d8d-a56c-c096e236ddcd") },
                    { new Guid("b6b45de4-6cf7-41b1-ad4b-3e12d3bc6b1b"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("71f80eef-d10e-41d2-a803-e5b0f563b0da") },
                    { new Guid("50cc28ba-4822-4980-928f-4cfd1bcf0f6e"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("8490dc09-50cd-42d4-87c6-b70ec7e488d5") },
                    { new Guid("170e5d4a-2637-48ee-abef-f37f3c25f602"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("b3ea5dcd-b6cf-48d5-90a3-6a0972cc72ad") },
                    { new Guid("62eb8a29-cbaa-4319-beff-4a18a57f5571"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("e1f523a6-0217-44a8-a649-a0d1b22505c3") },
                    { new Guid("d042d1c9-36dd-4336-9a51-08096df747d5"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"), new Guid("f9114f1d-d1a0-4afa-b1bc-b8137a5b7640") }
                });

            migrationBuilder.InsertData(
                table: "UnidadeAcessoModulo",
                columns: new[] { "Id", "DataCriacao", "Modulo", "UnidadeAcessoId" },
                values: new object[] { new Guid("0a78804c-fd66-4dca-ab3e-439811e46b93"), new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local), "Configuracao", new Guid("a89ee7c9-01c5-4387-95ab-dfef58eac490") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GrupoId",
                table: "AspNetUsers",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsersRefreshToken_AspNetUsersId",
                table: "AspNetUsersRefreshToken",
                column: "AspNetUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_AspNetUsersId",
                table: "Auditoria",
                column: "AspNetUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoAspNetUsers_AspNetUsersId",
                table: "GrupoAspNetUsers",
                column: "AspNetUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoAspNetUsers_GrupoId",
                table: "GrupoAspNetUsers",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoModulo_GrupoId",
                table: "GrupoModulo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUnidadeAcesso_UnidadeAcessoId",
                table: "GrupoUnidadeAcesso",
                column: "UnidadeAcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametroSistema_AspNetUsersId",
                table: "ParametroSistema",
                column: "AspNetUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoGrupo_GrupoId",
                table: "PermissaoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoGrupo_PermissaoId",
                table: "PermissaoGrupo",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuario_AspNetUsersId",
                table: "PermissaoUsuario",
                column: "AspNetUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuario_PermissaoId",
                table: "PermissaoUsuario",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAcessoModulo_UnidadeAcessoId",
                table: "UnidadeAcessoModulo",
                column: "UnidadeAcessoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsersRefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "GrupoAspNetUsers");

            migrationBuilder.DropTable(
                name: "GrupoModulo");

            migrationBuilder.DropTable(
                name: "GrupoUnidadeAcesso");

            migrationBuilder.DropTable(
                name: "ParametroSistema");

            migrationBuilder.DropTable(
                name: "PermissaoGrupo");

            migrationBuilder.DropTable(
                name: "PermissaoUsuario");

            migrationBuilder.DropTable(
                name: "UnidadeAcessoModulo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "UnidadeAcesso");

            migrationBuilder.DropTable(
                name: "Grupo");
        }
    }
}
