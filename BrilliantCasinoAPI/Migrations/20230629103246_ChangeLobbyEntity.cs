using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLobbyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountToStart",
                table: "Lobby",
                newName: "PlayersCount");

            migrationBuilder.AddColumn<int>(
                name: "PlayersAmountToStart",
                table: "Lobby",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayersAmountToStart",
                table: "Lobby");

            migrationBuilder.RenameColumn(
                name: "PlayersCount",
                table: "Lobby",
                newName: "AmountToStart");
        }
    }
}
