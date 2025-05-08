using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAth.Migrations
{
    /// <inheritdoc />
    public partial class timestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "timeStamp",
                table: "UserAccounts",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "timeStamp",
                table: "UserAccounts");
        }
    }
}
