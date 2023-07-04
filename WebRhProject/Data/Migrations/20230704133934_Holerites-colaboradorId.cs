using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class HoleritescolaboradorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dependentes",
                table: "TbHolerite",
                newName: "DependentesHolerite");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "TbHolerite",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DependentesHolerite",
                table: "TbHolerite",
                newName: "Dependentes");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "TbHolerite",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
