#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250201192746_RemoveVolumeFromIndexEntity")]
public class RemoveVolumeFromIndexEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "volume1",
            schema: "shd",
            table: "indices");

        migrationBuilder.RenameColumn(
            "volume",
            schema: "shd",
            table: "indices",
            newName: "value");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            "value",
            schema: "shd",
            table: "indices",
            newName: "volume");

        migrationBuilder.AddColumn<decimal>(
            "volume1",
            schema: "shd",
            table: "indices",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);
    }
}