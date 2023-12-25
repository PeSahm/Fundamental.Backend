namespace Fundamental.Infrastructure.Persistence.Repositories.Base;

public class FundamentalRepository<TEntity>(FundamentalDbContext context) : Repository<TEntity>(context)
    where TEntity : class;