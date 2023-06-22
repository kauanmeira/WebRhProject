using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class EnderecoAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "TbUsuario");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "TbColaborador",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCompleto",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario",
                column: "ColaboradorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "EnderecoCompleto",
                table: "TbColaborador");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "TbUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuario_ColaboradorId",
                table: "TbUsuario",
                column: "ColaboradorId");
        }
    }
}
