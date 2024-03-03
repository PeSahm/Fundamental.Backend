#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddFairEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ex_areas");

            migrationBuilder.CreateTable(
                name: "fair",
                schema: "ex_areas",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    json = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fair", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fair_id",
                schema: "ex_areas",
                table: "fair",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fair",
                schema: "ex_areas");
        }
    }
}