using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class Colaboradorbooleano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TbColaborador");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "TbColaborador",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "TbColaborador");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TbColaborador",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
