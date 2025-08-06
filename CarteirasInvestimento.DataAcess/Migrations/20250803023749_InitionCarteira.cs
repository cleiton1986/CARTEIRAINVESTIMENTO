using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteirasInvestimento.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class InitionCarteira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ativo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<int>(type: "INT", nullable: false),
                    Quantidade = table.Column<int>(type: "INT", nullable: false),
                    Codigo = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 20, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "DECIMAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    Logadouro = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<int>(type: "INT", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "VARCHAR(10)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Complemento = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carteira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AtivoId = table.Column<int>(type: "INT", nullable: false),
                    ClienteId = table.Column<int>(type: "INT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carteira_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtivoCarteira",
                columns: table => new
                {
                    AtivosId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarteirasId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtivoCarteira", x => new { x.AtivosId, x.CarteirasId });
                    table.ForeignKey(
                        name: "FK_AtivoCarteira_Ativo_AtivosId",
                        column: x => x.AtivosId,
                        principalTable: "Ativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtivoCarteira_Carteira_CarteirasId",
                        column: x => x.CarteirasId,
                        principalTable: "Carteira",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtivoCarteira_CarteirasId",
                table: "AtivoCarteira",
                column: "CarteirasId");

            migrationBuilder.CreateIndex(
                name: "IX_Carteira_ClienteId",
                table: "Carteira",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtivoCarteira");

            migrationBuilder.DropTable(
                name: "Ativo");

            migrationBuilder.DropTable(
                name: "Carteira");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
