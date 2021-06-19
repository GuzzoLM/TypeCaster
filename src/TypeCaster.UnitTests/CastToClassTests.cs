using NUnit.Framework;
using TypeCaster.TestClasses;

namespace TypeCaster.UnitTests
{
    public class CastToClassTests
    {
        [Test]
        public void ConvertClassAToClassB()
        {
            // Arrange
            var classA = TestClassA.RandomClass();

            var expectedClassB = new TestClassB
            {
                Id = classA.Id,
                Name = classA.Name,
                Age = classA.Age,
                Animal = classA.Animal.ToString(),
                Vehicle = classA.Vehicle.FromString(),
                Year = (int)classA.Year,
                IsCorrect = classA.IsCorrect ? 1 : 0,
                IsIncorrect = classA.IsIncorrect == 1
            };

            // Act
            var resultedClassB = CastToClass<TestClassB>.From(classA);

            if (expectedClassB.IsIncorrect != resultedClassB.IsIncorrect)
            {
                Assert.AreEqual(expectedClassB.IsIncorrect, resultedClassB.IsIncorrect);
            }

            // Assert
            Assert.AreEqual(expectedClassB.Id, resultedClassB.Id);
            Assert.AreEqual(expectedClassB.Name, resultedClassB.Name);
            Assert.AreEqual(expectedClassB.Age, resultedClassB.Age);
            Assert.AreEqual(expectedClassB.Animal, resultedClassB.Animal);
            Assert.AreEqual(expectedClassB.Vehicle, resultedClassB.Vehicle);
            Assert.AreEqual(expectedClassB.Year, resultedClassB.Year);
            Assert.AreEqual(expectedClassB.IsCorrect, resultedClassB.IsCorrect);
            Assert.AreEqual(expectedClassB.IsIncorrect, resultedClassB.IsIncorrect);
        }

        [Test]
        public void ConvertClassBToClassA()
        {
            // Arrange
            var expectedClassA = TestClassA.RandomClass();

            var classB = new TestClassB
            {
                Id = expectedClassA.Id,
                Name = expectedClassA.Name,
                Age = expectedClassA.Age,
                Animal = expectedClassA.Animal.ToString(),
                Vehicle = expectedClassA.Vehicle.FromString(),
                Year = (int)expectedClassA.Year,
                IsCorrect = expectedClassA.IsCorrect ? 1 : 0,
                IsIncorrect = expectedClassA.IsIncorrect == 1
            };

            // Act
            var resultedClassA = CastToClass<TestClassA>.From(classB);

            // Assert
            Assert.AreEqual(expectedClassA.Id, resultedClassA.Id);
            Assert.AreEqual(expectedClassA.Name, resultedClassA.Name);
            Assert.AreEqual(expectedClassA.Age, resultedClassA.Age);
            Assert.AreEqual(expectedClassA.Animal, resultedClassA.Animal);
            Assert.AreEqual(expectedClassA.Vehicle, resultedClassA.Vehicle);
            Assert.AreEqual(expectedClassA.Year, resultedClassA.Year);
            Assert.AreEqual(expectedClassA.IsCorrect, resultedClassA.IsCorrect);
            Assert.AreEqual(expectedClassA.IsIncorrect, resultedClassA.IsIncorrect);
        }
    }
}