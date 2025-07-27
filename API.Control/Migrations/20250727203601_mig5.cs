using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Control.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Inventories",
                newName: "Hardware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hardware",
                table: "Inventories",
                newName: "Data");
        }
    }
}
