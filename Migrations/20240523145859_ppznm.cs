using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deli.Migrations
{
    /// <inheritdoc />
    public partial class ppznm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appsettingss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Twitterlink = table.Column<string>(type: "text", nullable: true),
                    Instalink = table.Column<string>(type: "text", nullable: true),
                    Facebooklink = table.Column<string>(type: "text", nullable: true),
                    Linkedinlink = table.Column<string>(type: "text", nullable: true),
                    DeliPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DeliEmail = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appsettingss", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appsettingss");
        }
    }
}
