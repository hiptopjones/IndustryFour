using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddNextPreviousChunkId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "next_chunk_id",
                table: "chunks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "previous_chunk_id",
                table: "chunks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "next_chunk_id",
                table: "chunks");

            migrationBuilder.DropColumn(
                name: "previous_chunk_id",
                table: "chunks");
        }
    }
}
