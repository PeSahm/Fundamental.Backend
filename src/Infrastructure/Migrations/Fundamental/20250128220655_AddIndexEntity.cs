#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250128220655_AddIndexEntity")]
public class AddIndexEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "indices",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                date = table.Column<DateOnly>("date", nullable: false),
                open = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                high = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                low = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                volume = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                volume1 = table.Column<decimal>("numeric", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_indices", x => x._id);
                table.ForeignKey(
                    "fk_indices_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "ix_indices_id",
            schema: "shd",
            table: "indices",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_indices_symbol_id",
            schema: "shd",
            table: "indices",
            column: "symbol_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "indices",
            "shd");
    }
}