using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Control.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDeployProfile_ProfileDeploys_ProfileDeploysId",
                table: "ApplicationDeployProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ProfileDeploys_ProfileDeployId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceDriverPack");

            migrationBuilder.RenameColumn(
                name: "ProfileDeployId",
                table: "Devices",
                newName: "DeployProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_ProfileDeployId",
                table: "Devices",
                newName: "IX_Devices_DeployProfileId");

            migrationBuilder.RenameColumn(
                name: "ProfileDeploysId",
                table: "ApplicationDeployProfile",
                newName: "DeployProfilesId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDeployProfile_ProfileDeploysId",
                table: "ApplicationDeployProfile",
                newName: "IX_ApplicationDeployProfile_DeployProfilesId");

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "DriverPacks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeployTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeployProfileId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeployTask_ProfileDeploys_DeployProfileId",
                        column: x => x.DeployProfileId,
                        principalTable: "ProfileDeploys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverPacks_DeviceId",
                table: "DriverPacks",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployTask_DeployProfileId",
                table: "DeployTask",
                column: "DeployProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDeployProfile_ProfileDeploys_DeployProfilesId",
                table: "ApplicationDeployProfile",
                column: "DeployProfilesId",
                principalTable: "ProfileDeploys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ProfileDeploys_DeployProfileId",
                table: "Devices",
                column: "DeployProfileId",
                principalTable: "ProfileDeploys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverPacks_Devices_DeviceId",
                table: "DriverPacks",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDeployProfile_ProfileDeploys_DeployProfilesId",
                table: "ApplicationDeployProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ProfileDeploys_DeployProfileId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverPacks_Devices_DeviceId",
                table: "DriverPacks");

            migrationBuilder.DropTable(
                name: "DeployTask");

            migrationBuilder.DropIndex(
                name: "IX_DriverPacks_DeviceId",
                table: "DriverPacks");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DriverPacks");

            migrationBuilder.RenameColumn(
                name: "DeployProfileId",
                table: "Devices",
                newName: "ProfileDeployId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DeployProfileId",
                table: "Devices",
                newName: "IX_Devices_ProfileDeployId");

            migrationBuilder.RenameColumn(
                name: "DeployProfilesId",
                table: "ApplicationDeployProfile",
                newName: "ProfileDeploysId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDeployProfile_DeployProfilesId",
                table: "ApplicationDeployProfile",
                newName: "IX_ApplicationDeployProfile_ProfileDeploysId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDeployProfile_ProfileDeploys_ProfileDeploysId",
                table: "ApplicationDeployProfile",
                column: "ProfileDeploysId",
                principalTable: "ProfileDeploys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ProfileDeploys_ProfileDeployId",
                table: "Devices",
                column: "ProfileDeployId",
                principalTable: "ProfileDeploys",
                principalColumn: "Id");
        }
    }
}
