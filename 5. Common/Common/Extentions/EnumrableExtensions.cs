using Common.Unknown;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extentions
{
    public static class EnumrableExtensions
    {
        public static TResult[] ConvertArray<TSource, TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> toResult)
        {
            return items == null ? EmptyArray<TResult>.Instance : items.Select(toResult).ToArray();
        }

        public static IEnumerable<T> PageBy<T>(this IEnumerable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}
