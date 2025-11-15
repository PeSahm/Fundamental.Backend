#nullable disable

using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250125194849_AddStockOwnershipEntity")]
public class AddStockOwnershipEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "stock_ownership",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                parent_symbol_id = table.Column<long>("bigint", nullable: false),
                subsidiary_symbol_name = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                ownership_percentage = table.Column<decimal>("numeric(4,2)", nullable: false),
                subsidiary_symbol_id = table.Column<long>("bigint", nullable: true),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                cost_price = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_stock_ownership", x => x._id);
                table.ForeignKey(
                    "fk_stock_ownership_symbols_parent_symbol_id",
                    x => x.parent_symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "fk_stock_ownership_symbols_subsidiary_symbol_id",
                    x => x.subsidiary_symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id");
            });

        migrationBuilder.CreateIndex(
            "ix_stock_ownership_id",
            schema: "manufacturing",
            table: "stock_ownership",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_stock_ownership_parent_symbol_id",
            schema: "manufacturing",
            table: "stock_ownership",
            column: "parent_symbol_id");

        migrationBuilder.CreateIndex(
            "ix_stock_ownership_subsidiary_symbol_id",
            schema: "manufacturing",
            table: "stock_ownership",
            column: "subsidiary_symbol_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "stock_ownership",
            "manufacturing");
    }
}