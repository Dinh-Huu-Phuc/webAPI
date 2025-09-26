using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAPIStore.Migrations
{
    /// <inheritdoc />
    public partial class AddUnique_BookAuthor_and_PublisherName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Authors_AuthorId",
                table: "Books_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Books_BookId",
                table: "Books_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books_Authors",
                table: "Books_Authors");

            migrationBuilder.RenameTable(
                name: "Books_Authors",
                newName: "Book_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Authors_BookId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Authors_AuthorId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors");

            migrationBuilder.RenameTable(
                name: "Book_Authors",
                newName: "Books_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_BookId",
                table: "Books_Authors",
                newName: "IX_Books_Authors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_AuthorId",
                table: "Books_Authors",
                newName: "IX_Books_Authors_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books_Authors",
                table: "Books_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Authors_AuthorId",
                table: "Books_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Books_BookId",
                table: "Books_Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
