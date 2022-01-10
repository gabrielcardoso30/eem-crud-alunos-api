using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class AlunoEResponsavel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Segmento = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    FotoUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsavel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnidadeAcessoId = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Deletado = table.Column<bool>(nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(nullable: true),
                    UsuarioIdUltimaAtualizacao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    AlunoId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Parentesco = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Telefone = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsavel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_AlunoId",
                table: "Responsavel",
                column: "AlunoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsavel");

            migrationBuilder.DropTable(
                name: "Aluno");
        }
    }
}
