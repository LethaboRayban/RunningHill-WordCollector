using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordCollector.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueWordAndWorkTypeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Words_Type",
                table: "Words",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Word",
                table: "Words",
                column: "Word",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordTypes_Type",
                table: "Words",
                column: "Type",
                principalTable: "WordTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordTypes_Type",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_Type",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_Word",
                table: "Words");
        }
    }
}
