using System;
using System.Linq;

namespace Common.Extentions
{
    public static class QueryableExtensions
    {
        public static T[] MakeQueryToDatabase<T>(this IQueryable<T> source)
        {
            return source.ToArray();
        }

        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}
