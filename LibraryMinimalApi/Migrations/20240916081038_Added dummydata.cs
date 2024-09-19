using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Addeddummydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "Description", "Genre", "IsAvailable", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1, "Stephen King", "A horror novel about a haunted hotel.", 0, true, 1977, "The Shining" },
                    { 2, "J.R.R. Tolkien", "A fantasy novel about a hobbit's adventure.", 2, false, 1937, "The Hobbit" },
                    { 3, "Gillian Flynn", "A thriller about a woman's mysterious disappearance.", 1, true, 2012, "Gone Girl" },
                    { 4, "Jane Austen", "A classic romance about love and social standing.", 3, true, 1813, "Pride and Prejudice" },
                    { 5, "Dan Brown", "A mystery thriller about a secret society and ancient symbols.", 4, false, 2003, "The Da Vinci Code" },
                    { 6, "George Orwell", "A dystopian novel about a totalitarian regime.", 2, true, 1949, "1984" },
                    { 7, "Bram Stoker", "A horror novel about the legendary vampire Count Dracula.", 0, false, 1897, "Dracula" },
                    { 8, "Stieg Larsson", "A mystery thriller about a journalist and a hacker solving a cold case.", 4, true, 2005, "The Girl with the Dragon Tattoo" },
                    { 9, "Stephenie Meyer", "A romance fantasy novel about a girl falling in love with a vampire.", 3, true, 2005, "Twilight" },
                    { 10, "Suzanne Collins", "A dystopian novel about a fight to the death in a televised arena.", 2, false, 2008, "The Hunger Games" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 10);
        }
    }
}
