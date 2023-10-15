using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IcantHumor.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateRegistrationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "MediaFiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Dislike",
                table: "MediaFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "MediaFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
