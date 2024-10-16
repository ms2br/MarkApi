using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarkAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AllTableUpdateDeleteBehaviorCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteKeyword_Keywords_KeywordId",
                table: "NoteKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteKeyword_Notes_NoteId",
                table: "NoteKeyword");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteKeyword_Keywords_KeywordId",
                table: "NoteKeyword",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteKeyword_Notes_NoteId",
                table: "NoteKeyword",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteKeyword_Keywords_KeywordId",
                table: "NoteKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteKeyword_Notes_NoteId",
                table: "NoteKeyword");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteKeyword_Keywords_KeywordId",
                table: "NoteKeyword",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteKeyword_Notes_NoteId",
                table: "NoteKeyword",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
