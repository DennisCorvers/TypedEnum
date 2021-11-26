using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypedEnum;

namespace TypedEnumTest
{
    public class TypedEnumComparerTests
    {
        [Test]
        public void CompareEnumByteTest()
        {
            var value1 = new TypedEnum<EnumByte>(EnumByte.Value1);
            var value2 = new TypedEnum<EnumByte>(EnumByte.Value2);
            var value3 = new TypedEnum<EnumByte>(EnumByte.Value3);

            Assert.AreEqual(-1, value1.CompareTo(value2));
            Assert.AreEqual(0, value1.CompareTo(value1));
            Assert.AreEqual(1, value3.CompareTo(value2));
        }

        [Test]
        public void CompareEnumSByteTest()
        {
            var value1 = new TypedEnum<EnumSByte>(EnumSByte.Value1);
            var value2 = new TypedEnum<EnumSByte>(EnumSByte.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumUShortTest()
        {
            var value1 = new TypedEnum<EnumUShort>(EnumUShort.Value1);
            var value2 = new TypedEnum<EnumUShort>(EnumUShort.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumShortTest()
        {
            var value1 = new TypedEnum<EnumSByte>(EnumSByte.Value1);
            var value2 = new TypedEnum<EnumSByte>(EnumSByte.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumUIntTest()
        {
            var value1 = new TypedEnum<EnumSByte>(EnumSByte.Value1);
            var value2 = new TypedEnum<EnumSByte>(EnumSByte.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumIntTest()
        {
            var value1 = new TypedEnum<EnumInt>(EnumInt.Value1);
            var value2 = new TypedEnum<EnumInt>(EnumInt.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumULongTest()
        {
            var value1 = new TypedEnum<EnumULong>(EnumULong.Value1);
            var value2 = new TypedEnum<EnumULong>(EnumULong.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }

        [Test]
        public void CompareEnumLongTest()
        {
            var value1 = new TypedEnum<EnumLong>(EnumLong.Value1);
            var value2 = new TypedEnum<EnumLong>(EnumLong.Value2);

            Assert.AreEqual(-1, value1.CompareTo(value2));
        }
    }
}
