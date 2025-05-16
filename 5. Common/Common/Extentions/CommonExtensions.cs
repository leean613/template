using System;

namespace Common.Extentions
{
    public static class CommonExtensions
    {
        public static TOut IfNotNull<TIn, TOut>(this TIn value, Func<TIn, TOut> innerProperty, TOut otherwise)
            where TIn : class
        {
            return value == null ? otherwise : innerProperty(value);
        }
    }
}
