using Fundamental.Domain.Repositories.Base;
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
}