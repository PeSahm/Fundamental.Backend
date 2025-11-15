namespace Fundamental.Infrastructure.Persistence.Repositories.Base;

public class FundamentalRepository(FundamentalDbContext context) : RepositoryBase(context);