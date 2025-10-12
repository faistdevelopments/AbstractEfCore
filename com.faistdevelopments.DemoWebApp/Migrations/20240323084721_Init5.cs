using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.faistdevelopments.DemoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hobby",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hobby_UserId",
                table: "Hobby",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_User_UserId",
                table: "Hobby",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_User_UserId",
                table: "Hobby");

            migrationBuilder.DropIndex(
                name: "IX_Hobby_UserId",
                table: "Hobby");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hobby");
        }
    }
}
