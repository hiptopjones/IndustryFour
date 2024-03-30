using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenameConversationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turns_conversation_conversation_id",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_conversation",
                table: "conversation");

            migrationBuilder.RenameTable(
                name: "conversation",
                newName: "conversations");

            migrationBuilder.AlterColumn<string>(
                name: "request",
                table: "turns",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_conversations",
                table: "conversations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_turns_conversations_conversation_id",
                table: "turns",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turns_conversations_conversation_id",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_conversations",
                table: "conversations");

            migrationBuilder.RenameTable(
                name: "conversations",
                newName: "conversation");

            migrationBuilder.AlterColumn<string>(
                name: "request",
                table: "turns",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_conversation",
                table: "conversation",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_turns_conversation_conversation_id",
                table: "turns",
                column: "conversation_id",
                principalTable: "conversation",
                principalColumn: "id");
        }
    }
}
