using System;
using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class RemoveSectoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_symbol_sector_sector_id",
                schema: "shd",
                table: "symbol");

            migrationBuilder.DropTable(
                name: "sector",
                schema: "shd");

            migrationBuilder.DropIndex(
                name: "ix_symbol_sector_id",
                schema: "shd",
                table: "symbol");

            migrationBuilder.DropColumn(
                name: "sector_id",
                schema: "shd",
                table: "symbol");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sector_id",
                schema: "shd",
                table: "symbol",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "sector",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sector", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_symbol_sector_id",
                schema: "shd",
                table: "symbol",
                column: "sector_id");

            migrationBuilder.CreateIndex(
                name: "ix_sector_id",
                schema: "shd",
                table: "sector",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sector_name",
                schema: "shd",
                table: "sector",
                column: "name");

            migrationBuilder.AddForeignKey(
                name: "fk_symbol_sector_sector_id",
                schema: "shd",
                table: "symbol",
                column: "sector_id",
                principalSchema: "shd",
                principalTable: "sector",
                principalColumn: "_id");
        }
    }
}
