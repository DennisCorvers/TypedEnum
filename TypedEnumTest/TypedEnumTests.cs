using NUnit.Framework;
using System;
using TypedEnum;

namespace TypedEnumTest
{
    public class Tests
    {
        private const NUnitEnum testValue = NUnitEnum.InternalChange;
        private const NUnitEnum nonEqTestValue = NUnitEnum.BatchModeChange;
        private TypedEnum<NUnitEnum> testEnum;

        [SetUp]
        public void Setup()
        {
            testEnum = new TypedEnum<NUnitEnum>(testValue);
        }

        [Test]
        public void ValueTest()
        {
            Assert.AreEqual(testValue, testEnum.EnumValue);
        }

        [Test]
        public void EqualTypeOpTest()
        {
            var eq = new TypedEnum<NUnitEnum>(testValue);
            var nonEq = new TypedEnum<NUnitEnum>(nonEqTestValue);

            Assert.IsTrue(testEnum == eq);
            Assert.IsFalse(testEnum == nonEq);
        }

        [Test]
        public void NotEqualTypeOpTest()
        {
            var eq = new TypedEnum<NUnitEnum>(testValue);
            var nonEq = new TypedEnum<NUnitEnum>(nonEqTestValue);

            Assert.IsTrue(testEnum != nonEq);
            Assert.IsFalse(testEnum != eq);
        }

        [Test]
        public void EqualEnumOpTest()
        {
            var eq = testValue;
            var nonEq = nonEqTestValue;

            Assert.IsTrue(testEnum == eq);
            Assert.IsFalse(testEnum == nonEq);
        }

        [Test]
        public void NotEqualEnumOpTest()
        {
            var eq = testValue;
            var nonEq = nonEqTestValue;

            Assert.IsTrue(testEnum != nonEq);
            Assert.IsFalse(testEnum != eq);
        }

        [Test]
        public void EqualsTypeTest()
        {
            var eq = new TypedEnum<NUnitEnum>(testValue);
            var nonEq = new TypedEnum<NUnitEnum>(nonEqTestValue);

            Assert.IsTrue(testEnum.Equals(eq));
            Assert.IsFalse(testEnum.Equals(nonEq));
        }

        [Test]
        public void EqualsObjTest()
        {
            object eq = testValue;
            object nonEq = nonEqTestValue;
            object invalidObj = Math.PI;

            Assert.IsTrue(testEnum.Equals(new TypedEnum<NUnitEnum>(testValue)));
            Assert.IsFalse(testEnum.Equals(eq));
            Assert.IsFalse(testEnum.Equals(nonEq));
            Assert.IsFalse(testEnum.Equals(invalidObj));
        }

        [Test]
        public void CompareToObjTest()
        {
            Assert.AreEqual(1, testEnum.CompareTo(null));
            Assert.AreEqual(0, testEnum.CompareTo(new TypedEnum<NUnitEnum>(testValue)));
            Assert.Throws(typeof(ArgumentException), () =>
            {
                testEnum.CompareTo(1);
            });
        }

        [Test]
        public void GetHashCodeTest()
        {
            Assert.AreEqual(testValue.GetHashCode(), testEnum.GetHashCode());
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual(testValue.ToString(), testEnum.ToString());
        }
    }
}