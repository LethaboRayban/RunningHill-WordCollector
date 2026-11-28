using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WordCollector.Api.Migrations
{
    /// <inheritdoc />
    public partial class InialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Word = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WordTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Noun" },
                    { 1, "Verb" },
                    { 2, "Adjective" },
                    { 3, "Adverb" },
                    { 4, "Pronoun" },
                    { 5, "Preposition" },
                    { 6, "Interjection" },
                    { 7, "Conjunction" },
                    { 8, "Determiner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordTypes");
        }
    }
}
