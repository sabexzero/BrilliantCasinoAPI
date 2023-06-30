using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLobbyEntityAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayersCount",
                table: "Lobby");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayersCount",
                table: "Lobby",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
