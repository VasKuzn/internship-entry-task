using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace internship_entry_task.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    gameId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    currentPlayer = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    moveNumber = table.Column<int>(type: "integer", nullable: false),
                    gameField = table.Column<string>(type: "text", nullable: false),
                    gameState = table.Column<string>(type: "text", nullable: false),
                    conditions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.gameId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
