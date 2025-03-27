using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.TODO.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Todos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ContentId",
                table: "Todos",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Contents_ContentId",
                table: "Todos",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Contents_ContentId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Todos_ContentId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Todos");
        }
    }
}
