﻿using NModbus.Data;


namespace NModbus.UnitTests.Data
{
    public class RegisterCollectionFixture
    {
        [Test()]
        public void ByteCount()
        {
            RegisterCollection col = new RegisterCollection(1, 2, 3);
            Assert.AreEqual(6, col.ByteCount);
        }

        [Test()]
        public void NewRegisterCollection()
        {
            RegisterCollection col = new RegisterCollection(5, 3, 4, 6);
            Assert.NotNull(col);
            Assert.AreEqual(4, col.Count);
            Assert.AreEqual(5, col[0]);
        }

        [Test()]
        public void NewRegisterCollectionFromBytes()
        {
            RegisterCollection col = new RegisterCollection(new byte[] { 0, 1, 0, 2, 0, 3 });
            Assert.NotNull(col);
            Assert.AreEqual(3, col.Count);
            Assert.AreEqual(1, col[0]);
            Assert.AreEqual(2, col[1]);
            Assert.AreEqual(3, col[2]);
        }

        [Test()]
        public void RegisterCollectionNetworkBytes()
        {
            RegisterCollection col = new RegisterCollection(5, 3, 4, 6);
            byte[] bytes = col.NetworkBytes;
            Assert.NotNull(bytes);
            Assert.AreEqual(8, bytes.Length);
            Assert.AreEqual(new byte[] { 0, 5, 0, 3, 0, 4, 0, 6 }, bytes);
        }

        [Test()]
        public void RegisterCollectionEmpty()
        {
            RegisterCollection col = new RegisterCollection();
            Assert.NotNull(col);
            Assert.IsEmpty(col.NetworkBytes);
        }

        [Test()]
        public void ModifyRegister()
        {
            RegisterCollection col = new RegisterCollection(1, 2, 3, 4);
            col[0] = 5;
        }

        [Test()]
        public void AddRegister()
        {
            RegisterCollection col = new RegisterCollection();
            Assert.IsEmpty(col);
            col.Add(45);
            Assert.That(col, Has.Exactly(1).Items);
        }

        [Test()]
        public void RemoveRegister()
        {
            RegisterCollection col = new RegisterCollection(3, 4, 5);
            Assert.AreEqual(3, col.Count);
            col.RemoveAt(2);
            Assert.AreEqual(2, col.Count);
        }
    }
}
