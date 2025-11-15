using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251028072938_AddBalanceSheetDetailEntity")]
    public class AddBalanceSheetDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codal_category",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.DropColumn(
                name: "codal_row",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.DropColumn(
                name: "row",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.DropColumn(
                name: "value",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.CreateTable(
                name: "balance-sheet-detail",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance_sheet_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_category = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet_detail", x => x._id);
                    table.ForeignKey(
                        name: "fk_balance_sheet_detail_balance_sheet_balance_sheet_id",
                        column: x => x.balance_sheet_id,
                        principalSchema: "manufacturing",
                        principalTable: "balance-sheet",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_balance_sheet_id",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                column: "balance_sheet_id");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_id",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balance-sheet-detail",
                schema: "manufacturing");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.AddColumn<short>(
                name: "codal_category",
                schema: "manufacturing",
                table: "balance-sheet",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "codal_row",
                schema: "manufacturing",
                table: "balance-sheet",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "manufacturing",
                table: "balance-sheet",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "row",
                schema: "manufacturing",
                table: "balance-sheet",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "value",
                schema: "manufacturing",
                table: "balance-sheet",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                defaultValue: 0m);
        }
    }
}