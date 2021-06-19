using System;

namespace TypeCaster.TestClasses
{
    public class TestClassA
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TestEnumA Animal { get; set; }

        public string Vehicle { get; set; }

        public int Age { get; set; }

        public double Year { get; set; }

        public bool IsCorrect { get; set; }

        public int IsIncorrect { get; set; }

        public static TestClassA RandomClass() => new TestClassA
        {
            Id = RandomInteger(),
            Age = RandomInteger(),
            Animal = RandomEnum<TestEnumA>(),
            Name = Guid.NewGuid().ToString(),
            Vehicle = RandomEnum<TestEnumB>().ToString(),
            Year = RandomInteger(),
            IsCorrect = RandomBoolean(),
            IsIncorrect = RandomBoolean() ? 1 : 0
        };

        private static readonly Random Rand = new Random();

        private static int RandomInteger() => Rand.Next();

        private static T RandomEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(Rand.Next(values.Length));
        }

        private static bool RandomBoolean() => Rand.NextDouble() > 0.5;
    }
}