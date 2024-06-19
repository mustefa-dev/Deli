using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deli.Migrations
{
    /// <inheritdoc />
    public partial class facebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookAccessToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookId",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookAccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "Users");
        }
    }
}
