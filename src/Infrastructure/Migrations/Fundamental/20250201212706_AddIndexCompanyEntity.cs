using System;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20250201212706_AddIndexCompanyEntity")]
    public class AddIndexCompanyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "index_company",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    index_id = table.Column<long>(type: "bigint", nullable: false),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_index_company", x => x._id);
                    table.ForeignKey(
                        name: "fk_index_company_symbols_company_id",
                        column: x => x.company_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_index_company_symbols_index_id",
                        column: x => x.index_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_index_company_company_id",
                schema: "shd",
                table: "index_company",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_index_company_id",
                schema: "shd",
                table: "index_company",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_index_company_index_id",
                schema: "shd",
                table: "index_company",
                column: "index_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "index_company",
                schema: "shd");
        }
    }
}
