using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficio_TbHolerite_HoleriteId",
                table: "Beneficio");

            migrationBuilder.DropTable(
                name: "Desconto");

            migrationBuilder.DropIndex(
                name: "IX_Beneficio_HoleriteId",
                table: "Beneficio");

            migrationBuilder.DropColumn(
                name: "HoleriteId",
                table: "Beneficio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoleriteId",
                table: "Beneficio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Desconto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoleriteId = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desconto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desconto_TbHolerite_HoleriteId",
                        column: x => x.HoleriteId,
                        principalTable: "TbHolerite",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficio_HoleriteId",
                table: "Beneficio",
                column: "HoleriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Desconto_HoleriteId",
                table: "Desconto",
                column: "HoleriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficio_TbHolerite_HoleriteId",
                table: "Beneficio",
                column: "HoleriteId",
                principalTable: "TbHolerite",
                principalColumn: "Id");
        }
    }
}
