using NUnit.Framework;
using TypeCaster.UnitTests.Stub;

namespace TypeCaster.UnitTests
{
    public class CastToClassTests
    {
        [Test]
        public void ConvertClassAToClassB()
        {
            // Arrange
            var id = 1;
            var name = "MyClass";
            var type = TestEnum.TypeA;

            var classA = new TestClassA
            {
                Id = id,
                Name = name,
                Type = type
            };

            var expectedClassB = new TestClassB
            {
                Id = id,
                Name = name,
                Type = type.ToString()
            };

            // Act
            // var classB = classA.Cast<TestClassB>();
            var classB = CastToClass<TestClassB>.From(classA);

            // Assert

            Assert.AreEqual(expectedClassB.Id, classB.Id);
            Assert.AreEqual(expectedClassB.Name, classB.Name);
            Assert.AreEqual(expectedClassB.Type, classB.Type);
        }

        [Test]
        public void ConvertClassBToClassA()
        {
            // Arrange
            var id = 1;
            var name = "MyClass";
            var type = TestEnum.TypeA;

            var classB = new TestClassB
            {
                Id = id,
                Name = name,
                Type = type.ToString()
            };

            var expectedClassA = new TestClassA
            {
                Id = id,
                Name = name,
                Type = type
            };

            // Act
            // var classB = classA.Cast<TestClassB>();
            var classA = CastToClass<TestClassA>.From(classB);

            // Assert

            Assert.AreEqual(expectedClassA.Id, classA.Id);
            Assert.AreEqual(expectedClassA.Name, classA.Name);
            Assert.AreEqual(expectedClassA.Type, classA.Type);
        }
    }
}