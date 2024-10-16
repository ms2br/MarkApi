using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarkAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createdKeywordTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NormalizedWord = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keywords");
        }
    }
}
