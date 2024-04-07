using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNextPreviousChunkId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "next_chunk_id",
                table: "chunks");

            migrationBuilder.DropColumn(
                name: "previous_chunk_id",
                table: "chunks");

            migrationBuilder.AddColumn<string>(
                name: "embedded_text",
                table: "chunks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "embedded_text",
                table: "chunks");

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
    }
}
