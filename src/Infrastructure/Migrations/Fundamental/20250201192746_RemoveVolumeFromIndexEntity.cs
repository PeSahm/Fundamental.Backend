using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class RemoveVolumeFromIndexEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "volume1",
                schema: "shd",
                table: "indices");

            migrationBuilder.RenameColumn(
                name: "volume",
                schema: "shd",
                table: "indices",
                newName: "value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                schema: "shd",
                table: "indices",
                newName: "volume");

            migrationBuilder.AddColumn<decimal>(
                name: "volume1",
                schema: "shd",
                table: "indices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
