#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250201212706_AddIndexCompanyEntity")]
public class AddIndexCompanyEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "index_company",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                index_id = table.Column<long>("bigint", nullable: false),
                company_id = table.Column<long>("bigint", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_index_company", x => x._id);
                table.ForeignKey(
                    "fk_index_company_symbols_company_id",
                    x => x.company_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "fk_index_company_symbols_index_id",
                    x => x.index_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "ix_index_company_company_id",
            schema: "shd",
            table: "index_company",
            column: "company_id");

        migrationBuilder.CreateIndex(
            "ix_index_company_id",
            schema: "shd",
            table: "index_company",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_index_company_index_id",
            schema: "shd",
            table: "index_company",
            column: "index_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "index_company",
            "shd");
    }
}