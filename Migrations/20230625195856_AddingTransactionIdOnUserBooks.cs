using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS_Orientacao_Objetos.Migrations
{
    /// <inheritdoc />
    public partial class AddingTransactionIdOnUserBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_books",
                table: "user_books");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "user_books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_books",
                table: "user_books",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_books_UserId",
                table: "user_books",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_books",
                table: "user_books");

            migrationBuilder.DropIndex(
                name: "IX_user_books_UserId",
                table: "user_books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "user_books");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_books",
                table: "user_books",
                columns: new[] { "UserId", "BookId" });
        }
    }
}
