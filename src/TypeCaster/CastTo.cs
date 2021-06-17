using System;
using System.Linq.Expressions;

namespace TypeCaster
{
    /// <summary>
    /// Class to cast to type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    public static class CastTo<T>
    {
        /// <summary>
        /// Casts <see cref="TS"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="src">The source value to be casted.</param>
        /// <typeparam name="TS">Source type to cast from.</typeparam>
        /// <returns>Returns a value of type <see cref="T"/> with the same value <see cref="src"/>.</returns>
        public static T From<TS>(TS src)
        {
            try
            {
                return Cache<TS>.Caster(src);
            }
            catch (Exception)
            {
                if (typeof(T) == typeof(string))
                {
                    return From(src.ToString());
                }

                if (typeof(T).IsEnum && typeof(TS) == typeof(string))
                {
                    if (Enum.TryParse(typeof(T), src.ToString(), false, out var resultEnum))
                    {
                        return (T)resultEnum;
                    }
                }

                return default;
            }
        }

        private static class Cache<TS>
        {
            public static readonly Func<TS, T> Caster = Get();

            private static Func<TS, T> Get()
            {
                var p = Expression.Parameter(typeof(TS));
                var c = Expression.ConvertChecked(p, typeof(T));
                return Expression.Lambda<Func<TS, T>>(c, p).Compile();
            }
        }
    }
}