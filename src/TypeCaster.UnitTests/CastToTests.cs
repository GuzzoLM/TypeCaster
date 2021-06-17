using NUnit.Framework;
using TypeCaster.UnitTests.Stub;

namespace TypeCaster.UnitTests
{
    public class CastToTests
    {
        [Test]
        public void ConvertEnumToString()
        {
            var type = TestEnum.TypeA;

            var expectedString = type.ToString();

            // Act
            var resultString = CastTo<string>.From(type);

            Assert.AreEqual(expectedString, resultString);
        }

        [Test]
        public void ConvertStringToEnum()
        {
            var expectedType = TestEnum.TypeA;

            var typeString = "TypeA";

            // Act
            var resultType = CastTo<TestEnum>.From(typeString);

            Assert.AreEqual(expectedType, resultType);
        }
    }
}