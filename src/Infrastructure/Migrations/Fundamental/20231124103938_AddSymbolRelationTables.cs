using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    public partial class AddSymbolRelationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymbolRelations",
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
                        name: "FK_SymbolRelations_Symbols_ChildId",
                        column: x => x.ChildId,
                        principalSchema: "shd",
                        principalTable: "Symbols",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "FK_SymbolRelations_Symbols_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "shd",
                        principalTable: "Symbols",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_ChildId",
                table: "SymbolRelations",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_Id",
                table: "SymbolRelations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymbolRelations_ParentId",
                table: "SymbolRelations",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymbolRelations");
        }
    }
}
