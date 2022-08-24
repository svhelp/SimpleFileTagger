using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddLocationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationEntityId",
                table: "Tags",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    RootLocationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roots_Locations_RootLocationId",
                        column: x => x.RootLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LocationEntityId",
                table: "Tags",
                column: "LocationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                table: "Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RootId",
                table: "Locations",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_Roots_RootLocationId",
                table: "Roots",
                column: "RootLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Locations_LocationEntityId",
                table: "Tags",
                column: "LocationEntityId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Roots_RootId",
                table: "Locations",
                column: "RootId",
                principalTable: "Roots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Locations_LocationEntityId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Roots_RootId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Roots");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Tags_LocationEntityId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LocationEntityId",
                table: "Tags");
        }
    }
}
