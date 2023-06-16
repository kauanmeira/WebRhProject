using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class UsuariosColaboradorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColaboradorId",
                table: "TbUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario",
                column: "ColaboradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbUsuario_TbColaborador_ColaboradorId",
                table: "TbUsuario",
                column: "ColaboradorId",
                principalTable: "TbColaborador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbUsuario_TbColaborador_ColaboradorId",
                table: "TbUsuario");

            migrationBuilder.DropIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario");

            migrationBuilder.DropColumn(
                name: "ColaboradorId",
                table: "TbUsuario");
        }
    }
}
