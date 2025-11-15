using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHandling.AspNetCore;

public static class MvcOptionsExtensions
{
    public static MvcOptions AddDefaultResultConvention(this MvcOptions options)
    {
        options.Conventions.Add(new ResultConvention());

        return options;
    }
}