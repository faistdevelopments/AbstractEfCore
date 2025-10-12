using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.faistdevelopments.DemoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "HobbyUser",
                columns: table => new
                {
                    HobbiesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyUser", x => new { x.HobbiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_HobbyUser_Hobby_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyUser_UsersId",
                table: "HobbyUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbyUser");

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
    }
}
