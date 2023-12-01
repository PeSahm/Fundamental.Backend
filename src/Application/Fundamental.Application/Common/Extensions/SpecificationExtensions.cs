using System.Linq.Expressions;
using System.Reflection;
using Ardalis.Specification;

namespace Fundamental.Application.Common.Extensions;

public static class SpecificationExtensions
{
    public static IOrderedSpecificationBuilder<T> DynamicOrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        params string?[] orderBy
    )
    {
        bool isFirstOderBy = true;

        foreach (string? order in orderBy)
        {
            if (string.IsNullOrWhiteSpace(order))
            {
                continue;
            }

            string[] tokens = order.Split(' ');
            string propertyName = tokens[0];
            string sortDirection = tokens[1];

            if (isFirstOderBy)
            {
                OrderTypeEnum orderType = GetOrderType(sortDirection);

                ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<T>(GetOrderByExpression<T>(propertyName), orderType));
            }
            else
            {
                OrderTypeEnum thenByType = GetThenType(sortDirection);

                ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<T>(GetOrderByExpression<T>(propertyName), thenByType));
            }

            isFirstOderBy = false;
        }

        OrderedSpecificationBuilder<T> orderedSpecificationBuilder = new(specificationBuilder.Specification, false);

        return orderedSpecificationBuilder;
    }

    private static Expression<Func<T, object?>> GetOrderByExpression<T>(string orderBy)
    {
        // Split the string to get property name and sort direction
        string[] tokens = orderBy.Split(' ');
        string propertyName = tokens[0];

        // Get the property
        PropertyInfo? propertyInfo =
            typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (propertyInfo == null)
        {
            throw new ArgumentException($"No property '{propertyName}' found on type '{typeof(T).Name}'");
        }

        // Create the parameter expression
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        MemberExpression propertyExpression = Expression.Property(parameter, propertyInfo);

        // Convert the property expression to object as we're returning object?
        UnaryExpression convertExpression = Expression.Convert(propertyExpression, typeof(object));

        // Create and return the lambda expression
        return Expression.Lambda<Func<T, object?>>(convertExpression, parameter);
    }

    private static OrderTypeEnum GetOrderType(string sortDirection)
    {
        return sortDirection switch
        {
            "asc" => OrderTypeEnum.OrderBy,
            "desc" => OrderTypeEnum.OrderByDescending,
            _ => throw new ArgumentException($"Invalid sort direction '{sortDirection}'"),
        };
    }

    private static OrderTypeEnum GetThenType(string sortDirection)
    {
        return sortDirection switch
        {
            "asc" => OrderTypeEnum.ThenBy,
            "desc" => OrderTypeEnum.ThenByDescending,
            _ => throw new ArgumentException($"Invalid sort direction '{sortDirection}'"),
        };
    }
}