using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiMimic.Migrations
{
    public partial class BancoInicialII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Atualizado",
                table: "Palavras",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atualizado",
                table: "Palavras");
        }
    }
}
