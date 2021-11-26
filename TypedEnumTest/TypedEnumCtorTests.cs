using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypedEnum;

namespace TypedEnumTest
{
    public class TypedEnumCtorTests
    {
        [Test]
        public void CtorValid()
        {
            var e = new TypedEnum<NUnitEnum>(NUnitEnum.BatchMode);
            Assert.AreEqual(e.EnumValue, NUnitEnum.BatchMode);
        }

        [Test]
        public void CtorCastedValue()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                var e = new TypedEnum<NUnitEnum>((NUnitEnum)99999);
            });
        }

        [Test]
        public void CtorFlags()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                var e = new TypedEnum<NUnitEnum>(NUnitEnum.RefreshingProperties | NUnitEnum.ReInitTab);
            });
        }
    }
}
