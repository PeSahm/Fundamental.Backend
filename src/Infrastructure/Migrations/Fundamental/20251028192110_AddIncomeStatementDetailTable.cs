using System;
using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddIncomeStatementDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codal_category",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.DropColumn(
                name: "codal_row",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.DropColumn(
                name: "row",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.DropColumn(
                name: "value",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.AddColumn<DateTime>(
                name: "publish_date",
                schema: "manufacturing",
                table: "income_statement",
                type: "timestamp with time zone",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "income_statement_details",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_statement_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    codal_row = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_statement_details", x => x._id);
                    table.ForeignKey(
                        name: "fk_income_statement_details_income_statement_income_statement_",
                        column: x => x.income_statement_id,
                        principalSchema: "manufacturing",
                        principalTable: "income_statement",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_details_id",
                schema: "manufacturing",
                table: "income_statement_details",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_details_income_statement_id",
                schema: "manufacturing",
                table: "income_statement_details",
                column: "income_statement_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "income_statement_details",
                schema: "manufacturing");

            migrationBuilder.DropColumn(
                name: "publish_date",
                schema: "manufacturing",
                table: "income_statement");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.AddColumn<int>(
                name: "codal_category",
                schema: "manufacturing",
                table: "income_statement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "codal_row",
                schema: "manufacturing",
                table: "income_statement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "manufacturing",
                table: "income_statement",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "row",
                schema: "manufacturing",
                table: "income_statement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "value",
                schema: "manufacturing",
                table: "income_statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
