using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoCompleto",
                table: "TbColaborador");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "TbColaborador");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCompleto",
                table: "TbColaborador",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
