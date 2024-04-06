using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCaseNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chunks_documents_document_id",
                table: "chunks");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_categories_category_id",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_turns_conversations_conversation_id",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turns",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_documents",
                table: "documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_conversations",
                table: "conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chunks",
                table: "chunks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_turns_conversation_id",
                table: "turns",
                newName: "ix_turns_conversation_id");

            migrationBuilder.RenameIndex(
                name: "IX_documents_category_id",
                table: "documents",
                newName: "ix_documents_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_chunks_document_id",
                table: "chunks",
                newName: "ix_chunks_document_id");

            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "documents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_turns",
                table: "turns",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_documents",
                table: "documents",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_conversations",
                table: "conversations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chunks",
                table: "chunks",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_chunks_documents_document_id",
                table: "chunks",
                column: "document_id",
                principalTable: "documents",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_documents_categories_category_id",
                table: "documents",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_turns_conversations_conversation_id",
                table: "turns",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chunks_documents_document_id",
                table: "chunks");

            migrationBuilder.DropForeignKey(
                name: "fk_documents_categories_category_id",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "fk_turns_conversations_conversation_id",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "pk_turns",
                table: "turns");

            migrationBuilder.DropPrimaryKey(
                name: "pk_documents",
                table: "documents");

            migrationBuilder.DropPrimaryKey(
                name: "pk_conversations",
                table: "conversations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chunks",
                table: "chunks");

            migrationBuilder.DropPrimaryKey(
                name: "pk_categories",
                table: "categories");

            migrationBuilder.RenameIndex(
                name: "ix_turns_conversation_id",
                table: "turns",
                newName: "IX_turns_conversation_id");

            migrationBuilder.RenameIndex(
                name: "ix_documents_category_id",
                table: "documents",
                newName: "IX_documents_category_id");

            migrationBuilder.RenameIndex(
                name: "ix_chunks_document_id",
                table: "chunks",
                newName: "IX_chunks_document_id");

            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "documents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_turns",
                table: "turns",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documents",
                table: "documents",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_conversations",
                table: "conversations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chunks",
                table: "chunks",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chunks_documents_document_id",
                table: "chunks",
                column: "document_id",
                principalTable: "documents",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_categories_category_id",
                table: "documents",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_turns_conversations_conversation_id",
                table: "turns",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id");
        }
    }
}
