#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
public partial class UpdateToTimestampTz : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol-share-holders",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol-share-holders",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol-relation",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol-relation",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "fs",
            table: "publisher",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "fs",
            table: "publisher",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
            "tags",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "none_operational_income_tag[]",
            nullable: false,
            defaultValue: new List<NoneOperationalIncomeTag>(),
            oldClrType: typeof(List<NoneOperationalIncomeTag>),
            oldType: "none_operational_income_tag[]",
            oldDefaultValue: new List<NoneOperationalIncomeTag>());

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "monthly-activity",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "monthly-activity",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "indices",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "indices",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "index_company",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "index_company",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "income-statement-sort",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "income-statement-sort",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "income-statement",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "income-statement",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "financial-statement",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "financial-statement",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "ex_areas",
            table: "fair",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "ex_areas",
            table: "fair",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "close-price",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "close-price",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "balance-sheet",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "balance-sheet",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "Timestamp");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol-share-holders",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol-share-holders",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol-relation",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol-relation",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "symbol",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "symbol",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "fs",
            table: "publisher",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "fs",
            table: "publisher",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
            "tags",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "none_operational_income_tag[]",
            nullable: false,
            defaultValue: new List<NoneOperationalIncomeTag>(),
            oldClrType: typeof(List<NoneOperationalIncomeTag>),
            oldType: "none_operational_income_tag[]",
            oldDefaultValue: new List<NoneOperationalIncomeTag>());

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "monthly-activity",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "monthly-activity",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "indices",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "indices",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "index_company",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "index_company",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "income-statement-sort",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "income-statement-sort",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "income-statement",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "income-statement",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "financial-statement",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "financial-statement",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "ex_areas",
            table: "fair",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "ex_areas",
            table: "fair",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "shd",
            table: "close-price",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "shd",
            table: "close-price",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            "ModifiedAt",
            schema: "manufacturing",
            table: "balance-sheet",
            type: "Timestamp",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            "CreatedAt",
            schema: "manufacturing",
            table: "balance-sheet",
            type: "Timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }
}