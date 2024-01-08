#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddSymbolShareHolderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "SymbolShareHolder",
                    schema: "shd",
                    columns: table => new
                    {
                        _id = table.Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1")
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        SymbolId = table.Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        ShareHolderName = table.Column<string>(type: "nvarchar(512)", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        SharePercentage = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        ShareHolderSource = table.Column<short>(type: "SMALLINT", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        ReviewStatus = table.Column<short>(type: "SMALLINT", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        ShareHolderSymbolId = table.Column<long>(type: "bigint", nullable: true)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                        ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                            .Annotation("SqlServer:IsTemporal", true)
                            .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                            .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_SymbolShareHolder", x => x._id);
                        table.ForeignKey(
                            name: "FK_SymbolShareHolder_Symbol_ShareHolderSymbolId",
                            column: x => x.ShareHolderSymbolId,
                            principalSchema: "shd",
                            principalTable: "Symbol",
                            principalColumn: "_id");
                        table.ForeignKey(
                            name: "FK_SymbolShareHolder_Symbol_SymbolId",
                            column: x => x.SymbolId,
                            principalSchema: "shd",
                            principalTable: "Symbol",
                            principalColumn: "_id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolShareHolder_Id",
                schema: "shd",
                table: "SymbolShareHolder",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymbolShareHolder_ShareHolderSymbolId",
                schema: "shd",
                table: "SymbolShareHolder",
                column: "ShareHolderSymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolShareHolder_SymbolId",
                schema: "shd",
                table: "SymbolShareHolder",
                column: "SymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                    name: "SymbolShareHolder",
                    schema: "shd")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SymbolShareHolderHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "shd")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}