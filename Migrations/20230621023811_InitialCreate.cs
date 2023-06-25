using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS_Orientacao_Objetos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    phone = table.Column<string>(type: "VARCHAR(35)", nullable: false),
                    cpf = table.Column<string>(type: "CHAR(11)", nullable: false),
                    WriterLicense = table.Column<string>(type: "VARCHAR(35)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    phone = table.Column<string>(type: "VARCHAR(35)", nullable: false),
                    cpf = table.Column<string>(type: "CHAR(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isbn = table.Column<string>(type: "CHAR(17)", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(70)", nullable: false),
                    page_num = table.Column<int>(type: "INT", nullable: false),
                    publishing_date = table.Column<string>(type: "DATE", nullable: false),
                    is_rented = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    genre = table.Column<int>(type: "INTEGER", nullable: false),
                    donator = table.Column<int>(type: "INTEGER", nullable: false),
                    DonatedById = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                    table.ForeignKey(
                        name: "FK_books_genres_genre",
                        column: x => x.genre,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_users_DonatedById",
                        column: x => x.DonatedById,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "author_books",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_books", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_author_books_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_author_books_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_books",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    rental_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    rental_limit = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    returned_on = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_books", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_user_books_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_books_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_books_BookId",
                table: "author_books",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_books_DonatedById",
                table: "books",
                column: "DonatedById");

            migrationBuilder.CreateIndex(
                name: "IX_books_genre",
                table: "books",
                column: "genre");

            migrationBuilder.CreateIndex(
                name: "IX_user_books_BookId",
                table: "user_books",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author_books");

            migrationBuilder.DropTable(
                name: "user_books");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
