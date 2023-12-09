using Ripley.Api.Provider.Domain.Configuration.Pagination.Result;
using System.Linq.Expressions;
using System.Reflection;

namespace Ripley.Api.Provider.CrossCutting.Extensions.Pagination.QueryableExtension
{
    public static class QueryableExtensions
    {
        public static PaginationResultModel<T> GetPaged<T>(this IQueryable<T> query,
                                      int page, int pageSize) where T : class
        {
            var result = new PaginationResultModel<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();
            result.PageSize = result.Results.Count;

            return result;
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string order, string columnOrder)
        {
            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(columnOrder))
            {
                if (order.ToUpper() == "ASC")
                {
                    return ApplyOrder<T>(source, columnOrder, "OrderBy");
                }
                else
                {
                    return ApplyOrder<T>(source, columnOrder, "OrderByDescending");
                }
            }
            return source;
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
