using System;
using System.Linq.Expressions;

namespace TypeCaster
{
    public static class CastTo<T>
    {
        public static T From<S>(S src)
        {
            try
            {
                return Cache<S>.Caster(src);
            }
            catch (Exception)
            {
                if (typeof(T) == typeof(string))
                {
                    return From(src.ToString());
                }

                if (typeof(T).IsEnum && typeof(S) == typeof(string))
                {
                    if (Enum.TryParse(typeof(T), src.ToString(), false, out var resultEnum))
                    {
                        return (T)resultEnum;
                    }
                }

                return default;
            }
        }

        private static class Cache<S>
        {
            public static readonly Func<S, T> Caster = Get();

            private static Func<S, T> Get()
            {
                var p = Expression.Parameter(typeof(S));
                var c = Expression.ConvertChecked(p, typeof(T));
                return Expression.Lambda<Func<S, T>>(c, p).Compile();
            }
        }
    }
}