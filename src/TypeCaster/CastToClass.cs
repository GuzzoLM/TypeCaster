using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeCaster
{
    public static class CastToClass<T> where T : class
    {
        public static T From<S>(S src) where S : class
        {
            var srcMembers = Cache<S>.Members;

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

        private static class Cache<S>
        {

            public static readonly Dictionary<string, PropertyInfo> Members = GetMembers();

            private static Dictionary<string, PropertyInfo> GetMembers()
            {
                var srcType = typeof(S);

                return srcType.GetMembers()
                    .Where(x => x.MemberType == MemberTypes.Property)
                    .ToDictionary(x => x.Name, x => srcType.GetProperty(x.Name));
            }
        }
    }
}