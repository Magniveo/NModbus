using System;
using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadFileRecordResponseFixture
    {
        [Test()]
        public void Create()
        {
            var response = new WriteFileRecordResponse(17);
            Assert.AreEqual(ModbusFunctionCodes.WriteFileRecord, response.FunctionCode);
            Assert.AreEqual(17, response.SlaveAddress);
        }

        [Test()]
        public void CreateWithData()
        {
            var response = new WriteFileRecordResponse(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 }));
            Assert.AreEqual(ModbusFunctionCodes.WriteFileRecord, response.FunctionCode);
            Assert.AreEqual(17, response.SlaveAddress);
            Assert.AreEqual(1, response.Data.FileNumber);
            Assert.AreEqual(2, response.Data.StartingAddress);
            Assert.AreEqual(new byte[] { 4, 5 }, response.Data.DataBytes);
        }

        [Test()]
        public void Initialize()
        {
            var response = new WriteFileRecordResponse();
            response.Initialize(new byte[] {
                17, ModbusFunctionCodes.WriteFileRecord, 9, 6, 0, 1, 0, 2, 0, 1, 4, 5
            });

            Assert.AreEqual(ModbusFunctionCodes.WriteFileRecord, response.FunctionCode);
            Assert.AreEqual(17, response.SlaveAddress);
            Assert.AreEqual(1, response.Data.FileNumber);
            Assert.AreEqual(2, response.Data.StartingAddress);
            Assert.AreEqual(new byte[] { 4, 5 }, response.Data.DataBytes);
        }

        [Test()]
        public void ToString_Test()
        {
            var response = new WriteFileRecordResponse(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 }));

            Assert.AreEqual("Wrote 2 bytes for file 1 starting at address 2.", response.ToString());
        }
    }
}