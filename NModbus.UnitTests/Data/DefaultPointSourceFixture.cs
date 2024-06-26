using NModbus.Data;

namespace NModbus.UnitTests.Data
{

    public class DefaultPointSourceFixture
    {
        [Theory]
        [TestCase((ushort)0, 42)]
        [TestCase((ushort)(ushort.MaxValue - 1), 45)]
        [TestCase((ushort)77, 456)]
        [TestCase(ushort.MaxValue, 45123)]
        public void AddValues(ushort startAddress, int value)
        {
            IPointSource<int> points = new DefaultPointSource<int>();

            points.WritePoints(startAddress, new []{ value });

            Assert.AreEqual(value, points.ReadPoints(startAddress, 1)[0]);
        }

    }
}
