using System;
using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddStockOwnershipEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stock_ownership",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    subsidiary_symbol_name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ownership_percentage = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    subsidiary_symbol_id = table.Column<long>(type: "bigint", nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    cost_price = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_ownership", x => x._id);
                    table.ForeignKey(
                        name: "fk_stock_ownership_symbols_parent_symbol_id",
                        column: x => x.parent_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stock_ownership_symbols_subsidiary_symbol_id",
                        column: x => x.subsidiary_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_parent_symbol_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "parent_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_subsidiary_symbol_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "subsidiary_symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_ownership",
                schema: "manufacturing");
        }
    }
}
