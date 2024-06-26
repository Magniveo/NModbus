using NModbus.Data;
using System;

namespace NModbus.UnitTests.Data
{
    public class FileRecordCollectionFixture
    {
        [Test()]
        public void Constructor_ThrowsOddByteCount()
        {
            Assert.Throws<FormatException>(() => new FileRecordCollection(1, 2, new byte[] { 1, 2, 3 }));
        }

        [Test()]
        public void ByteCount()
        {
            var col = new FileRecordCollection(1, 2, new byte[] { 1, 2, 3, 4 });
            Assert.AreEqual(11, col.ByteCount);
        }

        [Test()]
        public void FileNumber()
        {
            var col = new FileRecordCollection(1, 2, new byte[] { 1, 2, 3, 4 });
            Assert.AreEqual(1, col.FileNumber);
        }

        [Test()]
        public void StartingAdress()
        {
            var col = new FileRecordCollection(1, 2, new byte[] { 1, 2, 3, 4 });
            Assert.AreEqual(2, col.StartingAddress);
        }

        [Test()]
        public void NetworkBytes()
        {
            var col = new FileRecordCollection(1, 3, new byte[] { 1, 2, 3,4  });
            Assert.AreEqual(new byte[] { 6, 0, 1, 0, 3, 0, 2, 1, 2, 3, 4 }, col.NetworkBytes);
        }
    }
}
