using System;

namespace TypeCaster.TestClasses
{
    public static class EnumExtensions
    {
        public static TestEnumB FromString(this string enumName)
        {
            return enumName switch
            {
                "Bicycle" => TestEnumB.Bicycle,
                "Motorcycle" => TestEnumB.Motorcycle,
                "Car" => TestEnumB.Car,
                "Truck" => TestEnumB.Truck,
                _ => throw new ArgumentOutOfRangeException(nameof(enumName), enumName, null)
            };
        }
    }
}