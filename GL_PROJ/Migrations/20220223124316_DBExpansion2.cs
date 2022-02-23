using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GL_PROJ.Migrations
{
    public partial class DBExpansion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_IconId",
                table: "Users",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Icons_IconId",
                table: "Users",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "IconId",
                onDelete: ReferentialAction.SetDefault);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Icons_IconId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IconId",
                table: "Users");
        }
    }
}
