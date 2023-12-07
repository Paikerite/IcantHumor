using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IcantHumor.Migrations
{
    /// <inheritdoc />
    public partial class FavouritesFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteOwners_Users_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPost = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavouriteOwnerViewModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_FavouriteOwners_FavouriteOwnerViewModelId",
                        column: x => x.FavouriteOwnerViewModelId,
                        principalTable: "FavouriteOwners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favourites_MediaFiles_IdPost",
                        column: x => x.IdPost,
                        principalTable: "MediaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteOwners_IdOwner",
                table: "FavouriteOwners",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_FavouriteOwnerViewModelId",
                table: "Favourites",
                column: "FavouriteOwnerViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_IdPost",
                table: "Favourites",
                column: "IdPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "FavouriteOwners");
        }
    }
}
