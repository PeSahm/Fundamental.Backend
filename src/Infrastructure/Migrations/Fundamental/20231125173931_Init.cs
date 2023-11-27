using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231125173931_Init")]
    public class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shd");

            migrationBuilder.CreateTable(
                name: "Symbol",
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
                    table.PrimaryKey("PK_Symbol", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "ClosePrice",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Close = table.Column<long>(type: "bigint", nullable: false),
                    Open = table.Column<long>(type: "bigint", nullable: false),
                    High = table.Column<long>(type: "bigint", nullable: false),
                    Low = table.Column<long>(type: "bigint", nullable: false),
                    Last = table.Column<long>(type: "bigint", nullable: false),
                    CloseAdjusted = table.Column<long>(type: "bigint", nullable: false),
                    OpenAdjusted = table.Column<long>(type: "bigint", nullable: false),
                    HighAdjusted = table.Column<long>(type: "bigint", nullable: false),
                    LowAdjusted = table.Column<long>(type: "bigint", nullable: false),
                    LastAdjusted = table.Column<long>(type: "bigint", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    SymbolId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosePrice", x => x._id);
                    table.ForeignKey(
                        name: "FK_ClosePrice_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SymbolRelations",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    Ratio = table.Column<double>(type: "float", nullable: false),
                    ChildId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolRelations", x => x._id);
                    table.ForeignKey(
                        name: "FK_SymbolRelations_Symbol_ChildId",
                        column: x => x.ChildId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "FK_SymbolRelations_Symbol_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosePrice_Date_SymbolId",
                schema: "shd",
                table: "ClosePrice",
                columns: new[] { "Date", "SymbolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosePrice_Id",
                schema: "shd",
                table: "ClosePrice",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosePrice_SymbolId",
                schema: "shd",
                table: "ClosePrice",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Symbol_Id",
                schema: "shd",
                table: "Symbol",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symbol_Isin",
                schema: "shd",
                table: "Symbol",
                column: "Isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symbol_Name",
                schema: "shd",
                table: "Symbol",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_ChildId",
                schema: "shd",
                table: "SymbolRelations",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_Id",
                schema: "shd",
                table: "SymbolRelations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_ParentId",
                schema: "shd",
                table: "SymbolRelations",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosePrice",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "SymbolRelations",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "Symbol",
                schema: "shd");
        }
    }
}