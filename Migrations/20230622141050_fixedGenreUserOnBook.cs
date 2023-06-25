using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS_Orientacao_Objetos.Migrations
{
    /// <inheritdoc />
    public partial class fixedGenreUserOnBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_users_DonatedById",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_DonatedById",
                table: "books");

            migrationBuilder.DropColumn(
                name: "DonatedById",
                table: "books");

            migrationBuilder.CreateIndex(
                name: "IX_books_donator",
                table: "books",
                column: "donator");

            migrationBuilder.AddForeignKey(
                name: "FK_books_users_donator",
                table: "books",
                column: "donator",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_users_donator",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_donator",
                table: "books");

            migrationBuilder.AddColumn<int>(
                name: "DonatedById",
                table: "books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_books_DonatedById",
                table: "books",
                column: "DonatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_books_users_DonatedById",
                table: "books",
                column: "DonatedById",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
