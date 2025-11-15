using System.Reflection;
using Coravel.Pro.EntityFramework;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Configuration.Fundamental;
using Microsoft.EntityFrameworkCore;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Infrastructure.Persistence;

public class FundamentalDbContext : DbContext, IUnitOfWork, ICoravelProDbContext
{
    public FundamentalDbContext()
    {
    }

    public FundamentalDbContext(DbContextOptions<FundamentalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Symbol> Symbols { get; set; }

    public DbSet<SymbolRelation> SymbolRelations { get; set; }

    public DbSet<RawMonthlyActivityJson> RawMonthlyActivityJsons { get; set; }

    public DbSet<CanonicalMonthlyActivity> CanonicalMonthlyActivities { get; set; }

    public DbSet<BalanceSheet> BalanceSheets { get; set; }

    public DbSet<BalanceSheetDetail> BalanceSheetDetails { get; set; }

    public DbSet<IncomeStatement> IncomeStatements { get; set; }

    public DbSet<IncomeStatementDetail> IncomeStatementDetails { get; set; }

    public DbSet<BalanceSheetSort> CodalRowOrders { get; set; }

    public DbSet<IncomeStatementSort> IncomeStatementSorts { get; set; }

    public DbSet<NonOperationIncomeAndExpense> NonOperationIncomeAndExpenses { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<SymbolShareHolder> SymbolShareHolders { get; set; }

    public DbSet<StockOwnership> StockOwnership { get; set; }

    public DbSet<Index> Indices { get; set; }

    public DbSet<IndexCompany> IndexCompanies { get; set; }

    public DbSet<FinancialStatement> ManufacturingFinancialStatement { get; set; }

    public DbSet<CapitalIncreaseRegistrationNotice> CapitalIncreaseRegistrationNotices { get; set; }

    public DbSet<Sector> Sectors { get; set; }

    public DbSet<CoravelJobHistory> Coravel_JobHistory { get; set; }
    public DbSet<CoravelScheduledJob> Coravel_ScheduledJobs { get; set; }
    public DbSet<CoravelScheduledJobHistory> Coravel_ScheduledJobHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace?.StartsWith(typeof(SymbolConfiguration).Namespace ??
                                               " Fundamental.Infrastructure.Configuration.Fundamental") ?? false
        );
    }
}