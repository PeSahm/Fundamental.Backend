using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231123180807_InitFundamental")]
    public class InitFundamental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shd");

            migrationBuilder.CreateTable(
                name: "Symbols",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Isin = table.Column<string>(type: "varchar(12)", nullable: false),
                    TseInsCode = table.Column<string>(type: "varchar(40)", nullable: false),
                    EnName = table.Column<string>(type: "varchar(100)", nullable: false),
                    SymbolEnName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CompanyEnCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CompanyPersianName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CompanyIsin = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MarketCap = table.Column<long>(type: "bigint", nullable: false),
                    SectorCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    SubSectorCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    ProductType = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_Id",
                schema: "shd",
                table: "Symbols",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_Isin",
                schema: "shd",
                table: "Symbols",
                column: "Isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_Name",
                schema: "shd",
                table: "Symbols",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbols",
                schema: "shd");
        }
    }
}