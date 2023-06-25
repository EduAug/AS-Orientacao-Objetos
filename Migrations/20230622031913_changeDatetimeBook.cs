using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS_Orientacao_Objetos.Migrations
{
    /// <inheritdoc />
    public partial class changeDatetimeBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "publishing_date",
                table: "books",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "DATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "publishing_date",
                table: "books",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");
        }
    }
}
