using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IcantHumor.Migrations
{
    /// <inheritdoc />
    public partial class AddedReactedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReactedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdReactedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChosenReact = table.Column<int>(type: "int", nullable: false),
                    MediaViewModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactedUsers_MediaFiles_MediaViewModelId",
                        column: x => x.MediaViewModelId,
                        principalTable: "MediaFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReactedUsers_MediaViewModelId",
                table: "ReactedUsers",
                column: "MediaViewModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactedUsers");
        }
    }
}
