using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NameID = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Argument = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Filter = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CategoryDescription = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppxPackageGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppxPackageGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppxPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Architecture = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PackageFamilyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PackageFullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsFramework = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBundle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsResourcePackage = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDevelopmentMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPartiallyStaged = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CategoryDescription = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
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
                    Model = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Phase = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationGroupMemberships",
                columns: table => new
                {
                    ApplicationGroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroupMemberships", x => new { x.ApplicationGroupsId, x.ApplicationsId });
                    table.ForeignKey(
                        name: "FK_ApplicationGroupMemberships_ApplicationGroups_ApplicationGroupsId",
                        column: x => x.ApplicationGroupsId,
                        principalTable: "ApplicationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationGroupMemberships_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppxPackageGroupMemberships",
                columns: table => new
                {
                    AppxPackageGroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppxPackagesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppxPackageGroupMemberships", x => new { x.AppxPackageGroupsId, x.AppxPackagesId });
                    table.ForeignKey(
                        name: "FK_AppxPackageGroupMemberships_AppxPackageGroups_AppxPackageGroupsId",
                        column: x => x.AppxPackageGroupsId,
                        principalTable: "AppxPackageGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppxPackageGroupMemberships_AppxPackages_AppxPackagesId",
                        column: x => x.AppxPackagesId,
                        principalTable: "AppxPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceModelApplications",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceModelsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModelApplications", x => new { x.ApplicationsId, x.DeviceModelsId });
                    table.ForeignKey(
                        name: "FK_DeviceModelApplications_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceModelApplications_DeviceModels_DeviceModelsId",
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
                    Source = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsOEM = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    Source = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ImageDescription = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    ImageIndex = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    EditionId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Languages = table.Column<string>(type: "TEXT", nullable: false),
                    ImageSize = table.Column<long>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    OperatingSystemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_OperatingSystems_OperatingSystemId",
                        column: x => x.OperatingSystemId,
                        principalTable: "OperatingSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeployProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ImageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationGroupId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AppxPackageGroupId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeployProfiles_ApplicationGroups_ApplicationGroupId",
                        column: x => x.ApplicationGroupId,
                        principalTable: "ApplicationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DeployProfiles_AppxPackageGroups_AppxPackageGroupId",
                        column: x => x.AppxPackageGroupId,
                        principalTable: "AppxPackageGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DeployProfiles_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeployProfileApplications",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeployProfilesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployProfileApplications", x => new { x.ApplicationsId, x.DeployProfilesId });
                    table.ForeignKey(
                        name: "FK_DeployProfileApplications_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeployProfileApplications_DeployProfiles_DeployProfilesId",
                        column: x => x.DeployProfilesId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeployProfileTasks",
                columns: table => new
                {
                    DeployProfilesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileTasksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployProfileTasks", x => new { x.DeployProfilesId, x.ProfileTasksId });
                    table.ForeignKey(
                        name: "FK_DeployProfileTasks_DeployProfiles_DeployProfilesId",
                        column: x => x.DeployProfilesId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeployProfileTasks_ProfileTasks_ProfileTasksId",
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
                    DeviceType = table.Column<string>(type: "TEXT", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MacAddress = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    DeviceModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeployProfileId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeployProfiles_DeployProfileId",
                        column: x => x.DeployProfileId,
                        principalTable: "DeployProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceApplications",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceApplications", x => new { x.ApplicationsId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_DeviceApplications_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceApplications_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceAppxPackages",
                columns: table => new
                {
                    AppxPackagesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevicesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceAppxPackages", x => new { x.AppxPackagesId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_DeviceAppxPackages_AppxPackages_AppxPackagesId",
                        column: x => x.AppxPackagesId,
                        principalTable: "AppxPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceAppxPackages_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDriverPacks",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriverPacksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverPacks", x => new { x.DeviceId, x.DriverPacksId });
                    table.ForeignKey(
                        name: "FK_DeviceDriverPacks_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDriverPacks_DriverPacks_DriverPacksId",
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
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
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
                name: "InventoryHardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    InventoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHardware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryHardware_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationGroupMemberships_ApplicationsId",
                table: "ApplicationGroupMemberships",
                column: "ApplicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationGroups_Name",
                table: "ApplicationGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_NameID",
                table: "Applications",
                column: "NameID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppxPackageGroupMemberships_AppxPackagesId",
                table: "AppxPackageGroupMemberships",
                column: "AppxPackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_AppxPackageGroups_Name",
                table: "AppxPackageGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppxPackages_PackageFullName",
                table: "AppxPackages",
                column: "PackageFullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfileApplications_DeployProfilesId",
                table: "DeployProfileApplications",
                column: "DeployProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfiles_ApplicationGroupId",
                table: "DeployProfiles",
                column: "ApplicationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfiles_AppxPackageGroupId",
                table: "DeployProfiles",
                column: "AppxPackageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfiles_ImageId",
                table: "DeployProfiles",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfiles_Name",
                table: "DeployProfiles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeployProfileTasks_ProfileTasksId",
                table: "DeployProfileTasks",
                column: "ProfileTasksId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceApplications_DevicesId",
                table: "DeviceApplications",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAppxPackages_DevicesId",
                table: "DeviceAppxPackages",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriverPacks_DriverPacksId",
                table: "DeviceDriverPacks",
                column: "DriverPacksId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModelApplications_DeviceModelsId",
                table: "DeviceModelApplications",
                column: "DeviceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModels_Manufacturer_Model",
                table: "DeviceModels",
                columns: new[] { "Manufacturer", "Model" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ComputerName",
                table: "Devices",
                column: "ComputerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeployProfileId",
                table: "Devices",
                column: "DeployProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceModelId",
                table: "Devices",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SerialNumber",
                table: "Devices",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverPacks_DeviceModelId_OS",
                table: "DriverPacks",
                columns: new[] { "DeviceModelId", "OS" });

            migrationBuilder.CreateIndex(
                name: "IX_Firmwares_DeviceModelId",
                table: "Firmwares",
                column: "DeviceModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_OperatingSystemId",
                table: "Images",
                column: "OperatingSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_DeviceId",
                table: "Inventories",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHardware_InventoryId",
                table: "InventoryHardware",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ShortName",
                table: "Manufacturers",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystems_Name",
                table: "OperatingSystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystems_ShortName",
                table: "OperatingSystems",
                column: "ShortName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationGroupMemberships");

            migrationBuilder.DropTable(
                name: "AppxPackageGroupMemberships");

            migrationBuilder.DropTable(
                name: "DeployProfileApplications");

            migrationBuilder.DropTable(
                name: "DeployProfileTasks");

            migrationBuilder.DropTable(
                name: "DeviceApplications");

            migrationBuilder.DropTable(
                name: "DeviceAppxPackages");

            migrationBuilder.DropTable(
                name: "DeviceDriverPacks");

            migrationBuilder.DropTable(
                name: "DeviceModelApplications");

            migrationBuilder.DropTable(
                name: "Firmwares");

            migrationBuilder.DropTable(
                name: "InventoryHardware");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "ProfileTasks");

            migrationBuilder.DropTable(
                name: "AppxPackages");

            migrationBuilder.DropTable(
                name: "DriverPacks");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DeployProfiles");

            migrationBuilder.DropTable(
                name: "DeviceModels");

            migrationBuilder.DropTable(
                name: "ApplicationGroups");

            migrationBuilder.DropTable(
                name: "AppxPackageGroups");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OperatingSystems");
        }
    }
}
