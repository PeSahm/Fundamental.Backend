#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20240303185448_AddFairEntity")]
public class AddFairEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "ex_areas");

        migrationBuilder.CreateTable(
            "fair",
            schema: "ex_areas",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                json = table.Column<string>("jsonb", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_fair", x => x._id);
            });

        migrationBuilder.CreateIndex(
            "ix_fair_id",
            schema: "ex_areas",
            table: "fair",
            column: "Id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "fair",
            "ex_areas");
    }
}