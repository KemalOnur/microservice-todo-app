using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.TODO.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTopic_Todo_TodoId",
                table: "TodoTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTopic_Topics_TopicId",
                table: "TodoTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTopic",
                table: "TodoTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todo",
                table: "Todo");

            migrationBuilder.RenameTable(
                name: "TodoTopic",
                newName: "TodoTopics");

            migrationBuilder.RenameTable(
                name: "Todo",
                newName: "Todos");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTopic_TopicId",
                table: "TodoTopics",
                newName: "IX_TodoTopics_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTopic_TodoId",
                table: "TodoTopics",
                newName: "IX_TodoTopics_TodoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTopics",
                table: "TodoTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todos",
                table: "Todos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTopics_Todos_TodoId",
                table: "TodoTopics",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTopics_Topics_TopicId",
                table: "TodoTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTopics_Todos_TodoId",
                table: "TodoTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTopics_Topics_TopicId",
                table: "TodoTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTopics",
                table: "TodoTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todos",
                table: "Todos");

            migrationBuilder.RenameTable(
                name: "TodoTopics",
                newName: "TodoTopic");

            migrationBuilder.RenameTable(
                name: "Todos",
                newName: "Todo");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTopics_TopicId",
                table: "TodoTopic",
                newName: "IX_TodoTopic_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTopics_TodoId",
                table: "TodoTopic",
                newName: "IX_TodoTopic_TodoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTopic",
                table: "TodoTopic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todo",
                table: "Todo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTopic_Todo_TodoId",
                table: "TodoTopic",
                column: "TodoId",
                principalTable: "Todo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTopic_Topics_TopicId",
                table: "TodoTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
