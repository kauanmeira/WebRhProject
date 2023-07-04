using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRhProject.Data.Migrations
{
    public partial class CPFColaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "TbUsuario");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiration",
                table: "TbUsuario");

            migrationBuilder.DropColumn(
                name: "TokenRedefinicaoSenha",
                table: "TbUsuario");

            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "TbHolerite",
                newName: "MesAno");

            migrationBuilder.RenameColumn(
                name: "DescIrrf",
                table: "TbHolerite",
                newName: "DescontoIRRF");

            migrationBuilder.RenameColumn(
                name: "DescInss",
                table: "TbHolerite",
                newName: "DescontoINSS");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "TbEmpresa",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CPF",
                table: "TbColaborador",
                type: "int",
                maxLength: 11,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "TbColaborador");

            migrationBuilder.RenameColumn(
                name: "MesAno",
                table: "TbHolerite",
                newName: "Mes");

            migrationBuilder.RenameColumn(
                name: "DescontoIRRF",
                table: "TbHolerite",
                newName: "DescIrrf");

            migrationBuilder.RenameColumn(
                name: "DescontoINSS",
                table: "TbHolerite",
                newName: "DescInss");

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "TbUsuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiration",
                table: "TbUsuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenRedefinicaoSenha",
                table: "TbUsuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "TbEmpresa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }
    }
}
