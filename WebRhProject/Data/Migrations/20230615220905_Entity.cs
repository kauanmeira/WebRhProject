using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "TbColaborador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TbEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEmpresa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbColaborador_EmpresaId",
                table: "TbColaborador",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbColaborador_TbEmpresa_EmpresaId",
                table: "TbColaborador",
                column: "EmpresaId",
                principalTable: "TbEmpresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbColaborador_TbEmpresa_EmpresaId",
                table: "TbColaborador");

            migrationBuilder.DropTable(
                name: "TbEmpresa");

            migrationBuilder.DropIndex(
                name: "IX_TbColaborador_EmpresaId",
                table: "TbColaborador");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "TbColaborador");
        }
    }
}
