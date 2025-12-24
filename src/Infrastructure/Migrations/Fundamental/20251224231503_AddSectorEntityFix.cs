using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddSectorEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create sector table if it doesn't exist (idempotent)
            migrationBuilder.Sql("""
                CREATE TABLE IF NOT EXISTS manufacturing.sector (
                    id uuid NOT NULL,
                    code text NOT NULL,
                    name text NOT NULL,
                    CONSTRAINT pk_sector PRIMARY KEY (id)
                );
                """);

            // Create unique index on sector.code if it doesn't exist (idempotent)
            migrationBuilder.Sql("""
                CREATE UNIQUE INDEX IF NOT EXISTS ix_sector_code
                ON manufacturing.sector (code);
                """);

            // Add sector_id column to symbol table if it doesn't exist (idempotent)
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'manufacturing'
                        AND table_name = 'symbol'
                        AND column_name = 'sector_id'
                    ) THEN
                        ALTER TABLE manufacturing.symbol
                        ADD COLUMN sector_id uuid NULL;
                    END IF;
                END $$;
                """);

            // Create index on symbol.sector_id if it doesn't exist (idempotent)
            migrationBuilder.Sql("""
                CREATE INDEX IF NOT EXISTS ix_symbol_sector_id
                ON manufacturing.symbol (sector_id);
                """);

            // Add foreign key constraint if it doesn't exist (idempotent)
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.table_constraints
                        WHERE table_schema = 'manufacturing'
                        AND table_name = 'symbol'
                        AND constraint_name = 'fk_symbol_sector_sector_id'
                    ) THEN
                        ALTER TABLE manufacturing.symbol
                        ADD CONSTRAINT fk_symbol_sector_sector_id
                        FOREIGN KEY (sector_id)
                        REFERENCES manufacturing.sector (id);
                    END IF;
                END $$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop FK constraint if exists
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.table_constraints
                        WHERE table_schema = 'manufacturing'
                        AND table_name = 'symbol'
                        AND constraint_name = 'fk_symbol_sector_sector_id'
                    ) THEN
                        ALTER TABLE manufacturing.symbol
                        DROP CONSTRAINT fk_symbol_sector_sector_id;
                    END IF;
                END $$;
                """);

            // Drop index if exists
            migrationBuilder.Sql("""
                DROP INDEX IF EXISTS manufacturing.ix_symbol_sector_id;
                """);

            // Drop sector_id column if exists
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'manufacturing'
                        AND table_name = 'symbol'
                        AND column_name = 'sector_id'
                    ) THEN
                        ALTER TABLE manufacturing.symbol
                        DROP COLUMN sector_id;
                    END IF;
                END $$;
                """);

            // Drop sector table if exists
            migrationBuilder.Sql("""
                DROP TABLE IF EXISTS manufacturing.sector;
                """);
        }
    }
}
