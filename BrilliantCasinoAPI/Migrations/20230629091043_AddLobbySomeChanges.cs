using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrilliantCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLobbySomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Lobby");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Lobby",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
