using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class empresaholeriteimplementação : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "TbHolerite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbHolerite_EmpresaId",
                table: "TbHolerite",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbHolerite_TbEmpresa_EmpresaId",
                table: "TbHolerite",
                column: "EmpresaId",
                principalTable: "TbEmpresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbHolerite_TbEmpresa_EmpresaId",
                table: "TbHolerite");

            migrationBuilder.DropIndex(
                name: "IX_TbHolerite_EmpresaId",
                table: "TbHolerite");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "TbHolerite");
        }
    }
}
