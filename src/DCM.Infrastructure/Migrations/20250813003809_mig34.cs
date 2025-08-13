using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHardware_Inventories_InventoryId1",
                table: "InventoryHardware");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHardware_InventoryId1",
                table: "InventoryHardware");

            migrationBuilder.RenameColumn(
                name: "InventoryId1",
                table: "InventoryHardware",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "InventoryHardware",
                type: "TEXT",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "InventoryHardware",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InventoryId",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "InventoryHardware",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHardware_InventoryId",
                table: "InventoryHardware",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHardware_Inventories_InventoryId",
                table: "InventoryHardware",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHardware_Inventories_InventoryId",
                table: "InventoryHardware");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHardware_InventoryId",
                table: "InventoryHardware");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InventoryHardware");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "InventoryHardware");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "InventoryHardware");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "InventoryHardware",
                newName: "InventoryId1");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "InventoryHardware",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "InventoryHardware",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InventoryHardware",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHardware_InventoryId1",
                table: "InventoryHardware",
                column: "InventoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHardware_Inventories_InventoryId1",
                table: "InventoryHardware",
                column: "InventoryId1",
                principalTable: "Inventories",
                principalColumn: "Id");
        }
    }
}
