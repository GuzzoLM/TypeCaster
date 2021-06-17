using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeCaster
{
    /// <summary>
    /// Cast to type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    public static class CastToClass<T> where T : class
    {
        /// <summary>
        /// Casts <see cref="TS"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="src">The source class to be casted.</param>
        /// <typeparam name="TS">Source type to cast from.</typeparam>
        /// <returns>Returns a class of type <see cref="T"/> with the same property values of the original <see cref="TS"/> class.</returns>
        public static T From<TS>(TS src) where TS : class
        {
            var srcMembers = SrcInfo<TS>.Members;

            var destType = typeof(T);

            var dest = Activator.CreateInstance(destType, false);

            foreach (var (key, propertyInfo) in Members)
            {
                var srcProperty = srcMembers[key];

                if (srcProperty?.PropertyType == propertyInfo.PropertyType)
                {
                    var value = srcProperty?.GetValue(src, null);
                    propertyInfo.SetValue(dest, value, null);
                }
                else
                {
                    var srcValue = srcProperty?.GetValue(src, null);

                    var generic = typeof(CastTo<>);
                    var castTo = generic.MakeGenericType(propertyInfo.PropertyType);
                    var method = castTo.GetMethod("From")?.MakeGenericMethod(srcProperty.PropertyType);

                    var value = method?.Invoke(null, new[] { srcValue });

                    propertyInfo.SetValue(dest, value, null);
                }
            }

            return (T)dest;
        }

        public static readonly Dictionary<string, PropertyInfo> Members = GetMembers();

        private static Dictionary<string, PropertyInfo> GetMembers()
        {
            var srcType = typeof(T);

            return srcType.GetMembers()
                .Where(x => x.MemberType == MemberTypes.Property)
                .ToDictionary(x => x.Name, x => srcType.GetProperty(x.Name));
        }

        private static class SrcInfo<TS>
        {
            public static readonly Dictionary<string, PropertyInfo> Members = GetMembers();

            private static Dictionary<string, PropertyInfo> GetMembers()
            {
                var srcType = typeof(TS);

                return srcType.GetMembers()
                    .Where(x => x.MemberType == MemberTypes.Property)
                    .ToDictionary(x => x.Name, x => srcType.GetProperty(x.Name));
            }
        }
    }
}