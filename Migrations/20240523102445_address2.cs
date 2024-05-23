﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deli.Migrations
{
    /// <inheritdoc />
    public partial class address2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Users",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");
        }
    }
}
