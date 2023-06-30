using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class needagainagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "LobbyId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "LobbyId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lobby_LobbyId",
                table: "AspNetUsers",
                column: "LobbyId",
                principalTable: "Lobby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
