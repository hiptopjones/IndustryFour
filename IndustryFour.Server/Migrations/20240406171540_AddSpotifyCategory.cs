using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSpotifyCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "Spotify Podcast" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
