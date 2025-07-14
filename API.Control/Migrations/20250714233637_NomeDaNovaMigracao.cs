using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Control.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaNovaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDriverPackage");

            migrationBuilder.DropTable(
                name: "DriverPackages");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "InventoryInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Inventories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Image",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "DeviceModels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DeviceDriverPack",
                columns: table => new
                {
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriverPacksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverPack", x => new { x.DevicesId, x.DriverPacksId });
                    table.ForeignKey(
                        name: "FK_DeviceDriverPack_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDriverPack_DriverPacks_DriverPacksId",
                        column: x => x.DriverPacksId,
                        principalTable: "DriverPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriverPack_DriverPacksId",
                table: "DeviceDriverPack",
                column: "DriverPacksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDriverPack");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "InventoryInfos");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "DeviceModels");

            migrationBuilder.CreateTable(
                name: "DriverPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    OS = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDriverPackage",
                columns: table => new
                {
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriverPackagesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverPackage", x => new { x.DevicesId, x.DriverPackagesId });
                    table.ForeignKey(
                        name: "FK_DeviceDriverPackage_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDriverPackage_DriverPackages_DriverPackagesId",
                        column: x => x.DriverPackagesId,
                        principalTable: "DriverPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriverPackage_DriverPackagesId",
                table: "DeviceDriverPackage",
                column: "DriverPackagesId");
        }
    }
}
