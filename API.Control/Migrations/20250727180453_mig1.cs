using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Control.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NameID = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Argument = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Filter = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppxPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Architecture = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PackageFamilyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PackageFullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsFramework = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBundle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsResourcePackage = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDevelopmentMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPartiallyStaged = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppxPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ImageDescription = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ImageIndex = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EditionId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Languages = table.Column<string>(type: "TEXT", nullable: false),
                    ImageSize = table.Column<long>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDeviceModel",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceModelsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDeviceModel", x => new { x.ApplicationsId, x.DeviceModelsId });
                    table.ForeignKey(
                        name: "FK_ApplicationDeviceModel_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationDeviceModel_DeviceModels_DeviceModelsId",
                        column: x => x.DeviceModelsId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OS = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverPacks_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firmwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firmwares_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeployProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeployProfiles_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PathsToCopy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeployProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProfileTasksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathsToCopy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathsToCopy_ProfileTasks_ProfileTasksId",
                        column: x => x.ProfileTasksId,
                        principalTable: "ProfileTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDeployProfile",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeployProfilesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDeployProfile", x => new { x.ApplicationsId, x.DeployProfilesId });
                    table.ForeignKey(
                        name: "FK_ApplicationDeployProfile_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationDeployProfile_DeployProfiles_DeployProfilesId",
                        column: x => x.DeployProfilesId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeployProfileProfileTask",
                columns: table => new
                {
                    DeployProfilesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileTasksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployProfileProfileTask", x => new { x.DeployProfilesId, x.ProfileTasksId });
                    table.ForeignKey(
                        name: "FK_DeployProfileProfileTask_DeployProfiles_DeployProfilesId",
                        column: x => x.DeployProfilesId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeployProfileProfileTask_ProfileTasks_ProfileTasksId",
                        column: x => x.ProfileTasksId,
                        principalTable: "ProfileTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MacAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeployProfileId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeployProfiles_DeployProfileId",
                        column: x => x.DeployProfileId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Devices_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDevice",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDevice", x => new { x.ApplicationsId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_ApplicationDevice_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationDevice_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppxPackageDevice",
                columns: table => new
                {
                    AppxPackagesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppxPackageDevice", x => new { x.AppxPackagesId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_AppxPackageDevice_AppxPackages_AppxPackagesId",
                        column: x => x.AppxPackagesId,
                        principalTable: "AppxPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppxPackageDevice_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDriverPack",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriverPacksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverPack", x => new { x.DeviceId, x.DriverPacksId });
                    table.ForeignKey(
                        name: "FK_DeviceDriverPack_Devices_DeviceId",
                        column: x => x.DeviceId,
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

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InventoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryInfos_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDeployProfile_DeployProfilesId",
                table: "ApplicationDeployProfile",
                column: "DeployProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDevice_DevicesId",
                table: "ApplicationDevice",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDeviceModel_DeviceModelsId",
                table: "ApplicationDeviceModel",
                column: "DeviceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppxPackageDevice_DevicesId",
                table: "AppxPackageDevice",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfileProfileTask_ProfileTasksId",
                table: "DeployProfileProfileTask",
                column: "ProfileTasksId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfiles_ImageId",
                table: "DeployProfiles",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriverPack_DriverPacksId",
                table: "DeviceDriverPack",
                column: "DriverPacksId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeployProfileId",
                table: "Devices",
                column: "DeployProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceModelId",
                table: "Devices",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverPacks_DeviceModelId",
                table: "DriverPacks",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmwares_DeviceModelId",
                table: "Firmwares",
                column: "DeviceModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_DeviceId",
                table: "Inventories",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInfos_InventoryId",
                table: "InventoryInfos",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PathsToCopy_ProfileTasksId",
                table: "PathsToCopy",
                column: "ProfileTasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDeployProfile");

            migrationBuilder.DropTable(
                name: "ApplicationDevice");

            migrationBuilder.DropTable(
                name: "ApplicationDeviceModel");

            migrationBuilder.DropTable(
                name: "AppxPackageDevice");

            migrationBuilder.DropTable(
                name: "DeployProfileProfileTask");

            migrationBuilder.DropTable(
                name: "DeviceDriverPack");

            migrationBuilder.DropTable(
                name: "Firmwares");

            migrationBuilder.DropTable(
                name: "InventoryInfos");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "PathsToCopy");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "AppxPackages");

            migrationBuilder.DropTable(
                name: "DriverPacks");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "ProfileTasks");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DeployProfiles");

            migrationBuilder.DropTable(
                name: "DeviceModels");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
