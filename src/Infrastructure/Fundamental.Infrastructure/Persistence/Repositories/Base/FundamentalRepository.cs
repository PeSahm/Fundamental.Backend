namespace Fundamental.Infrastructure.Persistence.Repositories.Base;

public class FundamentalRepository<TEntity> : Repository<TEntity>
    where TEntity : class
{
    public FundamentalRepository(FundamentalDbContext context)
        : base(context)
    {
    }
}