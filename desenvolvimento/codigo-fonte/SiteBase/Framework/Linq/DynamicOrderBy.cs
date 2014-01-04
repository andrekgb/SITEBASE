using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Linq
{
    /// <summary>
    /// Dynamic order by.
    /// </summary>
    public static class DynamicOrderBy
    {
        #region "Metodos"

        /// <summary>
        /// Order by dinâmico.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderByProperty"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, 
            string orderByProperty, bool desc) where TEntity : class
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            
            var type = typeof(TEntity);

            var arg = Expression.Parameter(type, "p");

            Expression expr = arg;

            string[] props = orderByProperty.Split('.');
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                System.Reflection.PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == command
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TEntity), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<TEntity>)result;
        }

        #endregion
    }
}