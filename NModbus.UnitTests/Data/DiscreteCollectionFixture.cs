using System;
using System.Linq;
using NModbus.Data;

namespace NModbus.UnitTests.Data
{
    public class DiscreteCollectionFixture
    {
        [Test()]
        public void ByteCount()
        {
            DiscreteCollection col = new DiscreteCollection(true, true, false, false, false, false, false, false, false);
            Assert.AreEqual(2, col.ByteCount);
        }

        [Test()]
        public void ByteCountEven()
        {
            DiscreteCollection col = new DiscreteCollection(true, true, false, false, false, false, false, false);
            Assert.AreEqual(1, col.ByteCount);
        }

        [Test()]
        public void NetworkBytes()
        {
            DiscreteCollection col = new DiscreteCollection(true, true);
            Assert.AreEqual(new byte[] { 3 }, col.NetworkBytes);
        }

        [Test()]
        public void CreateNewDiscreteCollectionInitialize()
        {
            DiscreteCollection col = new DiscreteCollection(true, true, true);
            Assert.AreEqual(3, col.Count);
            Assert.Contains(false, col);
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBoolParams()
        {
            DiscreteCollection col = new DiscreteCollection(true, false, true);
            Assert.AreEqual(3, col.Count);
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBytesParams()
        {
            DiscreteCollection col = new DiscreteCollection(1, 2, 3);
            Assert.AreEqual(24, col.Count);
            var expected = new bool[]
            {
                true, false, false, false, false, false, false, false,
                false, true, false, false, false, false, false, false,
                true, true, false, false, false, false, false, false,
            };

            Assert.AreEqual(expected, col);
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBytesParams_ZeroLengthArray()
        {
            DiscreteCollection col = new DiscreteCollection(new byte[0]);
            Assert.IsEmpty(col);
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBytesParams_NullArray()
        {
            Assert.Throws<ArgumentNullException>(() => new DiscreteCollection((byte[])null));
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBytesParamsOrder()
        {
            DiscreteCollection col = new DiscreteCollection(194);
            Assert.AreEqual(new bool[] { false, true, false, false, false, false, true, true }, col.ToArray());
        }

        [Test()]
        public void CreateNewDiscreteCollectionFromBytesParamsOrder2()
        {
            DiscreteCollection col = new DiscreteCollection(157, 7);
            Assert.AreEqual(
                new bool[]
                { true, false, true, true, true, false, false, true, true, true, true, false, false, false, false, false },
                col.ToArray());
        }

        [Test()]
        public void Resize()
        {
            DiscreteCollection col = new DiscreteCollection(byte.MaxValue, byte.MaxValue);
            Assert.AreEqual(16, col.Count);
            col.RemoveAt(3);
            Assert.AreEqual(15, col.Count);
        }

        [Test()]
        public void BytesPersistence()
        {
            DiscreteCollection col = new DiscreteCollection(byte.MaxValue, byte.MaxValue);
            Assert.AreEqual(16, col.Count);
            byte[] originalBytes = col.NetworkBytes;
            col.RemoveAt(3);
            Assert.AreEqual(15, col.Count);
            Assert.AreNotEqual(originalBytes, col.NetworkBytes);
        }

        [Test()]
        public void AddCoil()
        {
            DiscreteCollection col = new DiscreteCollection();
            Assert.IsEmpty(col);
            col.Add(true);
            Assert.That(col, Has.Exactly(1).Items);
        }
    }
}
