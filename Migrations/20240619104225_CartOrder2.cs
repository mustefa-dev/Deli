using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deli.Migrations
{
    /// <inheritdoc />
    public partial class CartOrder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_GovernorateId",
                table: "Users",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Governorates_GovernorateId",
                table: "Users",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Governorates_GovernorateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GovernorateId",
                table: "Users");
        }
    }
}
