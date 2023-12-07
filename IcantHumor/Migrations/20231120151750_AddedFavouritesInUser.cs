using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IcantHumor.Migrations
{
    /// <inheritdoc />
    public partial class AddedFavouritesInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_FavouriteOwners_FavouriteOwnerViewModelId",
                table: "Favourites");

            migrationBuilder.DropTable(
                name: "FavouriteOwners");

            migrationBuilder.RenameColumn(
                name: "FavouriteOwnerViewModelId",
                table: "Favourites",
                newName: "UserViewModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_FavouriteOwnerViewModelId",
                table: "Favourites",
                newName: "IX_Favourites_UserViewModelId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdReactedUser",
                table: "Favourites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "MediaFiles",
                columns: new[] { "Id", "DateUpload", "IdOfCreator", "Title", "TypeOfFile", "UrlToFile" },
                values: new object[,]
                {
                    { new Guid("09dd27b9-ff6a-4ff9-9d6d-00eb87385af5"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1363), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 34", 0, "PostResources\\bebis.jpg" },
                    { new Guid("0a3804ea-cb33-4c5a-8a0a-1b30cb2b33e8"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9993), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 6", 0, "PostResources\\bebis.jpg" },
                    { new Guid("1503ecfb-00a0-47c9-b963-70d5534c550f"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(852), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 23", 0, "PostResources\\bebis.jpg" },
                    { new Guid("19263425-49b9-4787-9754-7d9c4d52d4bf"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9726), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 2", 0, "PostResources\\bebis.jpg" },
                    { new Guid("1a4c0087-186b-480d-9da5-01af81b62656"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1269), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 32", 0, "PostResources\\bebis.jpg" },
                    { new Guid("1ed11aea-227f-45c1-89bc-4af8dbaf18f9"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1495), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 37", 0, "PostResources\\bebis.jpg" },
                    { new Guid("284c620b-64e0-4bf3-bd73-8903e3418220"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(635), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 19", 0, "PostResources\\bebis.jpg" },
                    { new Guid("2c3e80ef-c56f-4b0f-b064-4bf27c5a0e6e"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1175), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 30", 0, "PostResources\\bebis.jpg" },
                    { new Guid("3223c7f0-1250-4195-88a3-bd41d96028aa"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(305), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 12", 0, "PostResources\\bebis.jpg" },
                    { new Guid("3452e611-c6bb-4f65-a994-2adb34360ccd"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1130), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 29", 0, "PostResources\\bebis.jpg" },
                    { new Guid("4212d386-64b0-49ba-b82a-d5afbda7b931"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(110), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 8", 0, "PostResources\\bebis.jpg" },
                    { new Guid("44cc1920-16f2-457f-919b-994c64fe8bd7"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9823), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 4", 0, "PostResources\\bebis.jpg" },
                    { new Guid("4b19f793-d515-44a5-9a9d-0009526647d8"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(726), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 21", 0, "PostResources\\bebis.jpg" },
                    { new Guid("4d0755af-c019-4409-9219-a590f415e634"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(212), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 10", 0, "PostResources\\bebis.jpg" },
                    { new Guid("554cb396-bf9a-4f5e-8de5-7d4c15f9bd98"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1085), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 28", 0, "PostResources\\bebis.jpg" },
                    { new Guid("56d56a3a-a3f3-46ca-91a4-01d78e7ce604"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1407), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 35", 0, "PostResources\\bebis.jpg" },
                    { new Guid("5cd01024-9ddc-4d0a-b058-45db990c34e8"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(157), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 9", 0, "PostResources\\bebis.jpg" },
                    { new Guid("6d498141-2ee3-43f7-9e92-4a5adff7c621"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(440), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 15", 0, "PostResources\\bebis.jpg" },
                    { new Guid("83ee3afb-d2ef-426a-accc-273bfa36b124"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(950), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 25", 0, "PostResources\\bebis.jpg" },
                    { new Guid("8e90c18c-9b7c-48d3-9ead-026b8cf91c01"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(541), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 17", 0, "PostResources\\bebis.jpg" },
                    { new Guid("91af7f30-3d22-4a78-b5c6-3bf9dcb2f889"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(494), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 16", 0, "PostResources\\bebis.jpg" },
                    { new Guid("92330c7f-9141-475b-b99b-ab0d54d70b22"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9564), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 1", 0, "PostResources\\bebis.jpg" },
                    { new Guid("9d8c879a-1a2d-433c-9fed-e3ece139a88d"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(680), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 20", 0, "PostResources\\bebis.jpg" },
                    { new Guid("9f0bb2d9-b703-4058-b15c-dca91fe651a0"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1314), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 33", 0, "PostResources\\bebis.jpg" },
                    { new Guid("a2bef41c-9ba6-4687-8758-fbdef8c1e4ec"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(771), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 22", 0, "PostResources\\bebis.jpg" },
                    { new Guid("a613e5c4-d287-428f-ac5f-539fa64923d2"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(996), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 26", 0, "PostResources\\bebis.jpg" },
                    { new Guid("a77d4347-70ca-4bbb-85f6-4b0840937d53"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1041), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 27", 0, "PostResources\\bebis.jpg" },
                    { new Guid("ac2a9def-cefb-4a76-a85b-55c88ab438c0"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1450), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 36", 0, "PostResources\\bebis.jpg" },
                    { new Guid("c1dd14e4-9eb2-44ca-8b67-19ea7acc7c95"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1585), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 39", 0, "PostResources\\bebis.jpg" },
                    { new Guid("c5ce4831-dcd6-45ac-93cc-f347b1abc25c"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9869), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 5", 0, "PostResources\\bebis.jpg" },
                    { new Guid("c943b5bb-4e68-43f0-9b6f-1b44607fd8ef"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1539), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 38", 0, "PostResources\\bebis.jpg" },
                    { new Guid("cfaa7a1d-dba3-4191-b147-fbf23ad4e901"), new DateTime(2023, 11, 20, 17, 17, 48, 837, DateTimeKind.Local).AddTicks(9776), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 3", 0, "PostResources\\bebis.jpg" },
                    { new Guid("d410c337-dfe5-438a-a1b6-2f74f2f2ed45"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(395), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 14", 0, "PostResources\\bebis.jpg" },
                    { new Guid("d87c391d-210d-4b43-9001-31618b1ecd44"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(904), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 24", 0, "PostResources\\bebis.jpg" },
                    { new Guid("da424347-9f05-4a7e-91b8-7090582bbb72"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1634), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 40", 0, "PostResources\\bebis.jpg" },
                    { new Guid("dada5fce-6b68-44b5-a49a-964d8365f405"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(590), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 18", 0, "PostResources\\bebis.jpg" },
                    { new Guid("db432c1d-086d-4009-85af-377b1221d005"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(260), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 11", 0, "PostResources\\bebis.jpg" },
                    { new Guid("df370f79-4efa-4548-ad1a-2013fd2cbbd2"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(349), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 13", 0, "PostResources\\bebis.jpg" },
                    { new Guid("e030f940-6405-4b99-86fa-71f83a26afc4"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(1220), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 31", 0, "PostResources\\bebis.jpg" },
                    { new Guid("fa510361-d46b-46ff-a96a-341a2ddcac80"), new DateTime(2023, 11, 20, 17, 17, 48, 838, DateTimeKind.Local).AddTicks(38), new Guid("01001b92-a37e-48f5-4ccf-08dbe68eb722"), "funny cat 7", 0, "PostResources\\bebis.jpg" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Users_UserViewModelId",
                table: "Favourites",
                column: "UserViewModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Users_UserViewModelId",
                table: "Favourites");

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("09dd27b9-ff6a-4ff9-9d6d-00eb87385af5"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("0a3804ea-cb33-4c5a-8a0a-1b30cb2b33e8"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("1503ecfb-00a0-47c9-b963-70d5534c550f"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("19263425-49b9-4787-9754-7d9c4d52d4bf"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("1a4c0087-186b-480d-9da5-01af81b62656"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("1ed11aea-227f-45c1-89bc-4af8dbaf18f9"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("284c620b-64e0-4bf3-bd73-8903e3418220"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("2c3e80ef-c56f-4b0f-b064-4bf27c5a0e6e"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("3223c7f0-1250-4195-88a3-bd41d96028aa"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("3452e611-c6bb-4f65-a994-2adb34360ccd"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("4212d386-64b0-49ba-b82a-d5afbda7b931"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("44cc1920-16f2-457f-919b-994c64fe8bd7"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("4b19f793-d515-44a5-9a9d-0009526647d8"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("4d0755af-c019-4409-9219-a590f415e634"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("554cb396-bf9a-4f5e-8de5-7d4c15f9bd98"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("56d56a3a-a3f3-46ca-91a4-01d78e7ce604"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("5cd01024-9ddc-4d0a-b058-45db990c34e8"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("6d498141-2ee3-43f7-9e92-4a5adff7c621"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("83ee3afb-d2ef-426a-accc-273bfa36b124"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("8e90c18c-9b7c-48d3-9ead-026b8cf91c01"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("91af7f30-3d22-4a78-b5c6-3bf9dcb2f889"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("92330c7f-9141-475b-b99b-ab0d54d70b22"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("9d8c879a-1a2d-433c-9fed-e3ece139a88d"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("9f0bb2d9-b703-4058-b15c-dca91fe651a0"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("a2bef41c-9ba6-4687-8758-fbdef8c1e4ec"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("a613e5c4-d287-428f-ac5f-539fa64923d2"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("a77d4347-70ca-4bbb-85f6-4b0840937d53"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("ac2a9def-cefb-4a76-a85b-55c88ab438c0"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("c1dd14e4-9eb2-44ca-8b67-19ea7acc7c95"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("c5ce4831-dcd6-45ac-93cc-f347b1abc25c"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("c943b5bb-4e68-43f0-9b6f-1b44607fd8ef"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("cfaa7a1d-dba3-4191-b147-fbf23ad4e901"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("d410c337-dfe5-438a-a1b6-2f74f2f2ed45"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("d87c391d-210d-4b43-9001-31618b1ecd44"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("da424347-9f05-4a7e-91b8-7090582bbb72"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("dada5fce-6b68-44b5-a49a-964d8365f405"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("db432c1d-086d-4009-85af-377b1221d005"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("df370f79-4efa-4548-ad1a-2013fd2cbbd2"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("e030f940-6405-4b99-86fa-71f83a26afc4"));

            migrationBuilder.DeleteData(
                table: "MediaFiles",
                keyColumn: "Id",
                keyValue: new Guid("fa510361-d46b-46ff-a96a-341a2ddcac80"));

            migrationBuilder.DropColumn(
                name: "IdReactedUser",
                table: "Favourites");

            migrationBuilder.RenameColumn(
                name: "UserViewModelId",
                table: "Favourites",
                newName: "FavouriteOwnerViewModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_UserViewModelId",
                table: "Favourites",
                newName: "IX_Favourites_FavouriteOwnerViewModelId");

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

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteOwners_IdOwner",
                table: "FavouriteOwners",
                column: "IdOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_FavouriteOwners_FavouriteOwnerViewModelId",
                table: "Favourites",
                column: "FavouriteOwnerViewModelId",
                principalTable: "FavouriteOwners",
                principalColumn: "Id");
        }
    }
}
