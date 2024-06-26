using System;
using System.IO;
using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadFileRecordResquestFixture
    {
        [Test()]
        public void Create()
        {
            var request = new WriteFileRecordRequest(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 } ));
            Assert.AreEqual(ModbusFunctionCodes.WriteFileRecord, request.FunctionCode);
            Assert.AreEqual(17, request.SlaveAddress);
            Assert.AreEqual(1, request.Data.FileNumber);
            Assert.AreEqual(2, request.Data.StartingAddress);
            Assert.AreEqual(new byte[] { 4, 5 }, request.Data.DataBytes);
        }

        [Test()]
        public void Validate_ThrowsOnFileNumberMismatch()
        {
            var request = new WriteFileRecordRequest(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 }));
            var response = new WriteFileRecordResponse(17, new FileRecordCollection(2, 2, new byte[] { 4, 5 }));
            Assert.Throws<IOException>(() => request.ValidateResponse(response));
        }

        [Test()]
        public void Validate_ThrowsOnStartingAddressMismatch()
        {
            var request = new WriteFileRecordRequest(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 }));
            var response = new WriteFileRecordResponse(17, new FileRecordCollection(1, 4, new byte[] { 4, 5 }));
            Assert.Throws<IOException>(() => request.ValidateResponse(response));
        }

        [Test()]
        public void Initialize()
        {
            var response = new WriteFileRecordRequest();
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
            var request = new WriteFileRecordRequest(17, new FileRecordCollection(1, 2, new byte[] { 4, 5 }));

            Assert.AreEqual("Write 2 bytes for file 1 starting at address 2.", request.ToString());
        }
    }
}