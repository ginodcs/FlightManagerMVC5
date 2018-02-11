using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ARQ.Maqueta.Services
{
    public static class IQueryableExtensions
    {
        #region OrderBy extensions

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IQueryable<T> ThenBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = GetProperty(type, prop);
                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
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
            return (IQueryable<T>)result;
        }

        public static IQueryable<T> GetOrdering<T>(IQueryable<T> query, List<OrderBy> orderBy)
        {
            bool isFirst = true;

            foreach (OrderBy order in orderBy)
            {
                if (isFirst)
                {
                    if (order.Ascending)
                    {
                        query = query.OrderBy(order.Property);
                    }
                    else
                    {
                        query = query.OrderByDescending(order.Property);
                    }
                }
                else
                {
                    if (order.Ascending)
                    {
                        query = query.ThenBy(order.Property);
                    }
                    else
                    {
                        query = query.ThenByDescending(order.Property);
                    }
                }

                isFirst = false;
            }

            return query;
        }

        public static IQueryable<T> Includes<T>(this IQueryable<T> source, string[] includes) where T : class
        {
            foreach (var item in includes)
            {
                source = source.Include(item);
            }

            return source;
        }

        private static PropertyInfo GetProperty(Type type, string prop)
        {
            PropertyInfo pi = type.GetProperty(prop);
            return pi == null && type.BaseType != null ? GetProperty(type.BaseType, prop) : pi;
        }

        #endregion
    }
}
