using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetFit.Migrations
{
    /// <inheritdoc />
    public partial class gymUserNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_AspNetUsers_AppUserId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_Homes_AspNetUsers_AppUserId",
                table: "Homes");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Homes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Gyms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_AspNetUsers_AppUserId",
                table: "Gyms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_AspNetUsers_AppUserId",
                table: "Homes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_AspNetUsers_AppUserId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_Homes_AspNetUsers_AppUserId",
                table: "Homes");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Homes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Gyms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_AspNetUsers_AppUserId",
                table: "Gyms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_AspNetUsers_AppUserId",
                table: "Homes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
