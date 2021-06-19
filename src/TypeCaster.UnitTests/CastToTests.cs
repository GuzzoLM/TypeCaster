using NUnit.Framework;
using TypeCaster.TestClasses;

namespace TypeCaster.UnitTests
{
    public class CastToTests
    {
        [Test]
        public void ConvertEnumToString([Values] TestEnumA type)
        {
            var expectedString = type.ToString();

            // Act
            var resultString = CastTo<string>.From(type);

            Assert.AreEqual(expectedString, resultString);
        }

        [Test]
        public void ConvertStringToEnum([Values] TestEnumB expectedType)
        {
            var typeString = expectedType.ToString();

            // Act
            var resultType = CastTo<TestEnumB>.From(typeString);

            Assert.AreEqual(expectedType, resultType);
        }

        [Test]
        public void ConvertIntToBoolean([Values] bool expectedBoolean)
        {
            // Assert
            var testInt = expectedBoolean ? 1 : 0;

            // Act
            var resultedBoolean = CastTo<bool>.From(testInt);

            // Assert
            Assert.AreEqual(expectedBoolean, resultedBoolean);
        }

        [Test]
        public void ConvertBooleanToInt([Values] bool testBoolean)
        {
            // Assert
            var expectedInt = testBoolean ? 1 : 0;

            // Act
            var resultedInt = CastTo<int>.From(testBoolean);

            // Assert
            Assert.AreEqual(expectedInt, resultedInt);
        }
    }
}