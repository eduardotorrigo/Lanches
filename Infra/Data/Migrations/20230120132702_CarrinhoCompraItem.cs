using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Infra.Data.Migrations
{
    public partial class CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LancheId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CarrinhoCompraId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraItens", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItens_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1169), new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1195) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1204), new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1211), new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1213) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 4,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1218), new DateTime(2023, 1, 20, 10, 27, 1, 590, DateTimeKind.Local).AddTicks(1221) });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItens_LancheId",
                table: "CarrinhoCompraItens",
                column: "LancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItens");

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5573), new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5594) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5601), new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5603) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5607), new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5608) });

            migrationBuilder.UpdateData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 4,
                columns: new[] { "CreatedOn", "EditedOn" },
                values: new object[] { new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5612), new DateTime(2023, 1, 18, 17, 59, 50, 747, DateTimeKind.Local).AddTicks(5614) });
        }
    }
}
