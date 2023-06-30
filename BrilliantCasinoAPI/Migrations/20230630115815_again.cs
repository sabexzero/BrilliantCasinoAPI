using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LobbyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PlayersCount",
                table: "Lobby",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayersCount",
                table: "Lobby");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LobbyId",
                table: "AspNetUsers",
                column: "LobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers",
                column: "LobbyId",
                principalTable: "Lobby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
