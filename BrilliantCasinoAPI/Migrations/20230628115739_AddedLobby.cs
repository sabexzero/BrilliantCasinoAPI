using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedLobby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LobbyId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lobby",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AmountToStart = table.Column<int>(type: "integer", nullable: false),
                    PlayersLimit = table.Column<int>(type: "integer", nullable: false),
                    Game = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobby", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LobbyId",
                table: "AspNetUsers",
                column: "LobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers",
                column: "LobbyId",
                principalTable: "Lobby",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lobby");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LobbyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LobbyId",
                table: "AspNetUsers");
        }
    }
}
