using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IcantHumor.Migrations
{
    /// <inheritdoc />
    public partial class AddedManyToManyCategoryAndMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MediaFiles_MediaViewModelId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MediaViewModelId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MediaViewModelId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryViewModelMediaViewModel",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryViewModelMediaViewModel", x => new { x.CategoriesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_CategoryViewModelMediaViewModel_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryViewModelMediaViewModel_MediaFiles_PostsId",
                        column: x => x.PostsId,
                        principalTable: "MediaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryViewModelMediaViewModel_PostsId",
                table: "CategoryViewModelMediaViewModel",
                column: "PostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryViewModelMediaViewModel");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaViewModelId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MediaViewModelId",
                table: "Categories",
                column: "MediaViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MediaFiles_MediaViewModelId",
                table: "Categories",
                column: "MediaViewModelId",
                principalTable: "MediaFiles",
                principalColumn: "Id");
        }
    }
}
