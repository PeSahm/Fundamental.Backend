using System.Reflection;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.Infrastructure.Configuration.ExAreas;
using Fundamental.Infrastructure.Configuration.Fundamental;
using Microsoft.EntityFrameworkCore;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Infrastructure.Persistence;

public class FundamentalDbContext : DbContext, IUnitOfWork
{
    public FundamentalDbContext()
    {
    }

    public FundamentalDbContext(DbContextOptions<FundamentalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Symbol> Symbols { get; set; } = null!;

    public DbSet<SymbolRelation> SymbolRelations { get; set; } = null!;

    public DbSet<MonthlyActivity> MonthlyActivities { get; set; } = null!;

    public DbSet<BalanceSheet> BalanceSheets { get; set; }

    public DbSet<IncomeStatement> IncomeStatements { get; set; }

    public DbSet<BalanceSheetSort> CodalRowOrders { get; set; }

    public DbSet<IncomeStatementSort> IncomeStatementSorts { get; set; }

    public DbSet<NonOperationIncomeAndExpense> NonOperationIncomeAndExpenses { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<SymbolShareHolder> SymbolShareHolders { get; set; }

    public DbSet<StockOwnership> StockOwnership { get; set; }

    public DbSet<Index> Indices { get; set; }

    public DbSet<IndexCompany> IndexCompanies { get; set; }

    public DbSet<FinancialStatement> ManufacturingFinancialStatement { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasPostgresEnum<IsoCurrency>();
        // modelBuilder.HasPostgresEnum<ReportingType>();
        // modelBuilder.HasPostgresEnum<CompanyType>();
        // modelBuilder.HasPostgresEnum<EnableSubCompany>();
        // modelBuilder.HasPostgresEnum<PublisherFundType>();
        // modelBuilder.HasPostgresEnum<PublisherSubCompanyType>();
        // modelBuilder.HasPostgresEnum<PublisherMarketType>();
        // modelBuilder.HasPostgresEnum<PublisherState>();
        // modelBuilder.HasPostgresEnum<ReviewStatus>();
        // modelBuilder.HasPostgresEnum<ProductType>();
        // modelBuilder.HasPostgresEnum<ExchangeType>();
        // modelBuilder.HasPostgresEnum<EtfType>();
        // modelBuilder.HasPostgresEnum<NoneOperationalIncomeTag>();

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace!.StartsWith(typeof(SymbolConfiguration).Namespace!) ||
                    type.Namespace!.StartsWith(typeof(FairConfiguration).Namespace!)
        );
    }
}