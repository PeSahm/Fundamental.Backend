using System.Reflection;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Statements.Entities;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Configuration.Fundamental;
using Microsoft.EntityFrameworkCore;

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

    public DbSet<FinancialStatement> FinancialStatements { get; set; } = null!;

    public DbSet<MonthlyActivity> MonthlyActivities { get; set; } = null!;

    public DbSet<BalanceSheet> BalanceSheets { get; set; }

    public DbSet<IncomeStatement> IncomeStatements { get; set; }

    public DbSet<NonOperationIncomeAndExpense> NonOperationIncomeAndExpenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace!.StartsWith(typeof(SymbolConfiguration).Namespace!));
    }
}