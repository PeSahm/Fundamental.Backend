#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231227041237_AddPublisherTable")]
    public class AddPublisherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUnOfficial",
                schema: "shd",
                table: "Symbol",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodalId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SymbolId = table.Column<long>(type: "bigint", nullable: false),
                    ParentSymbolId = table.Column<long>(type: "bigint", nullable: true),
                    Isic = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ReportingType = table.Column<int>(type: "int", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    ExecutiveManager = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TelNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    FaxNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ActivitySubject = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ShareOfficeAddress = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Inspector = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FinancialManager = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FactoryTel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    FactoryFax = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    OfficeTel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    OfficeFax = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ShareOfficeTel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ShareOfficeFax = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FinancialYear = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Currency = table.Column<string>(type: "char(3)", fixedLength: true, maxLength: 3, nullable: false),
                    AuditorName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IsEnableSubCompany = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    FundType = table.Column<int>(type: "int", nullable: false),
                    SubCompanyType = table.Column<int>(type: "int", nullable: false),
                    IsSupplied = table.Column<bool>(type: "bit", nullable: false),
                    MarketType = table.Column<int>(type: "int", nullable: false),
                    ListedCapital = table.Column<decimal>(type: "decimal(36,10)", precision: 36, scale: 10, nullable: false),
                    UnauthorizedCapital = table.Column<decimal>(type: "decimal(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x._id);
                    table.ForeignKey(
                        name: "FK_Publisher_Symbol_ParentSymbolId",
                        column: x => x.ParentSymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "FK_Publisher_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_Id",
                table: "Publisher",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_ParentSymbolId",
                table: "Publisher",
                column: "ParentSymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_SymbolId",
                table: "Publisher",
                column: "SymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropColumn(
                name: "IsUnOfficial",
                schema: "shd",
                table: "Symbol");
        }
    }
}