using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Infra.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EditedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EditedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    LancheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoCurta = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoLonga = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemThumbnailUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanchePreferido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmEstoque = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EditedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EditedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.LancheId);
                    table.ForeignKey(
                        name: "FK_Lanches_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CreatedBy", "CreatedOn", "Descricao", "EditedBy", "EditedOn", "Nome" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anche feito com ingredientes normais", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Normal" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CreatedBy", "CreatedOn", "Descricao", "EditedBy", "EditedOn", "Nome" },
                values: new object[] { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lanche feito com ingredientes integrais e naturais", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Natural" });

            migrationBuilder.InsertData(
                table: "Lanches",
                columns: new[] { "LancheId", "CategoriaId", "CreatedBy", "CreatedOn", "DescricaoCurta", "DescricaoLonga", "EditedBy", "EditedOn", "EmEstoque", "ImagemThumbnailUrl", "ImagemUrl", "LanchePreferido", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, 1, "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5573), "Pão, Hamburger, ovo, presunto, queijo e batata palha", "Delicioso hamburger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha", "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5594), true, null, "~/imagens/produtos/cheesesalada1.jpg", false, "Cheese Salada", 12.50m },
                    { 2, 1, "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5601), "Pão, presunto, mussarela e tomate", "Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho", "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5603), true, null, "~/imagens/produtos/mistoquente4.jpg", false, "Misto Quente", 8.0m },
                    { 3, 1, "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5607), "Pão, hambúrger, presunto, mussarela e batalha palha", "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha", "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5608), true, null, "~/imagens/produtos/cheeseburger1.jpg", false, "Cheese Burger", 11.0m },
                    { 4, 2, "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5612), "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte", "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural", "teste", new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5614), true, null, "~/imagens/produtos/lanchenatural.jpg", false, "anche Natural Peito Peru", 15.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lanches_CategoriaId",
                table: "Lanches",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
