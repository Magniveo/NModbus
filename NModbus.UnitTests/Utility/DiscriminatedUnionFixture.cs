using System;
using NModbus.Utility;


namespace NModbus.UnitTests.Utility
{
    public class DiscriminatedUnionFixture
    {
        [Test()]
        public void DiscriminatedUnion_CreateA()
        {
            var du = DiscriminatedUnion<string, string>.CreateA("foo");
            Assert.AreEqual(DiscriminatedUnionOption.A, du.Option);
            Assert.AreEqual("foo", du.A);
        }

        [Test()]
        public void DiscriminatedUnion_CreateB()
        {
            var du = DiscriminatedUnion<string, string>.CreateB("foo");
            Assert.AreEqual(DiscriminatedUnionOption.B, du.Option);
            Assert.AreEqual("foo", du.B);
        }

        [Test()]
        public void DiscriminatedUnion_AllowNulls()
        {
            var du = DiscriminatedUnion<object, object>.CreateB(null);
            Assert.AreEqual(DiscriminatedUnionOption.B, du.Option);
            Assert.Null(du.B);
        }

        [Test()]
        public void AccessInvalidOption_A()
        {
            var du = DiscriminatedUnion<string, string>.CreateB("foo");
            Assert.Throws<InvalidOperationException>(() => du.A.ToString());
        }

        [Test()]
        public void AccessInvalidOption_B()
        {
            var du = DiscriminatedUnion<string, string>.CreateA("foo");
            Assert.Throws<InvalidOperationException>(() => du.B.ToString());
        }

        [Test()]
        public void DiscriminatedUnion_ToString()
        {
            var du = DiscriminatedUnion<string, string>.CreateA("foo");
            Assert.AreEqual("foo", du.ToString());
        }
    }
}