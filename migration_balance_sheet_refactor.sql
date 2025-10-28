-- Migration script to refactor BalanceSheet table into BalanceSheet and BalanceSheetDetail tables
-- This script migrates existing data from the flat BalanceSheet table to the normalized structure

-- IMPORTANT: Backup your database before running this migration!
-- This script will permanently alter your data structure.

-- Step 1: Create the new BalanceSheetDetail table
CREATE TABLE IF NOT EXISTS "balance-sheet-detail" (
    "Id" uuid NOT NULL DEFAULT uuid_generate_v4(),
    "BalanceSheetId" uuid NOT NULL,
    "Row" integer NOT NULL,
    "CodalRow" integer NOT NULL,
    "CodalCategory" integer NOT NULL,
    "Description" text,
    "Value" numeric NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "PK_balance-sheet-detail" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_balance-sheet-detail_balance-sheet_BalanceSheetId" FOREIGN KEY ("BalanceSheetId") REFERENCES "balance-sheet" ("Id") ON DELETE CASCADE
);

-- Step 2: Create indexes for better performance
CREATE INDEX IF NOT EXISTS "IX_balance-sheet-detail_BalanceSheetId" ON "balance-sheet-detail" ("BalanceSheetId");
CREATE INDEX IF NOT EXISTS "IX_balance-sheet-detail_CodalCategory" ON "balance-sheet-detail" ("CodalCategory");
CREATE INDEX IF NOT EXISTS "IX_balance-sheet-detail_CodalRow" ON "balance-sheet-detail" ("CodalRow");

-- Step 3: Migrate existing data from BalanceSheet to BalanceSheetDetail
-- Insert detail records for each BalanceSheet record that has detail data
INSERT INTO "balance-sheet-detail" (
    "Id",
    "BalanceSheetId",
    "Row",
    "CodalRow",
    "CodalCategory",
    "Description",
    "Value",
    "CreatedAt"
)
SELECT
    uuid_generate_v4() as "Id",
    "Id" as "BalanceSheetId",
    "Row",
    "CodalRow",
    "CodalCategory",
    "Description",
    "Value",
    "CreatedAt"
FROM "balance-sheet"
WHERE "Row" IS NOT NULL; -- Only migrate rows that have detail data

-- Step 4: Remove the detail columns from the BalanceSheet table
-- Note: This will permanently remove the data, so ensure the migration above worked correctly
ALTER TABLE "balance-sheet" DROP COLUMN IF EXISTS "Row";
ALTER TABLE "balance-sheet" DROP COLUMN IF EXISTS "CodalRow";
ALTER TABLE "balance-sheet" DROP COLUMN IF EXISTS "CodalCategory";
ALTER TABLE "balance-sheet" DROP COLUMN IF EXISTS "Description";
ALTER TABLE "balance-sheet" DROP COLUMN IF EXISTS "Value";

-- Verification queries (run these after migration to verify data integrity):
-- SELECT COUNT(*) FROM "balance-sheet"; -- Should show original count of balance sheet headers
-- SELECT COUNT(*) FROM "balance-sheet-detail"; -- Should show total detail rows migrated
-- SELECT bs."Id", COUNT(bsd."Id") as DetailCount
-- FROM "balance-sheet" bs
-- LEFT JOIN "balance-sheet-detail" bsd ON bs."Id" = bsd."BalanceSheetId"
-- GROUP BY bs."Id"
-- ORDER BY DetailCount DESC LIMIT 10; -- Check distribution of details per balance sheet