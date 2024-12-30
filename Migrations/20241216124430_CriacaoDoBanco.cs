using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoVeiculo.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gestao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAvaliador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaCarro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloCarro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documentacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorFinanciamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorVenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opcionais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Multas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SenhaSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gestao");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
