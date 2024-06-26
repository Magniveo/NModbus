using System;
using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadHoldingInputRegistersResponseFixture
    {
        [Test()]
        public void ReadHoldingInputRegistersResponse_NullData()
        {
            Assert.Throws<ArgumentNullException>(() => new ReadHoldingInputRegistersResponse(0, 0, null));
        }

        [Test()]
        public void ReadHoldingRegistersResponse()
        {
            ReadHoldingInputRegistersResponse response =
                new ReadHoldingInputRegistersResponse(ModbusFunctionCodes.ReadHoldingRegisters, 5, new RegisterCollection(1, 2));
            Assert.AreEqual(ModbusFunctionCodes.ReadHoldingRegisters, response.FunctionCode);
            Assert.AreEqual(5, response.SlaveAddress);
            Assert.AreEqual(4, response.ByteCount);
            RegisterCollection col = new RegisterCollection(1, 2);
            Assert.AreEqual(col.NetworkBytes, response.Data.NetworkBytes);
        }

        [Test()]
        public void ToString_ReadHoldingRegistersResponse()
        {
            ReadHoldingInputRegistersResponse response =
                new ReadHoldingInputRegistersResponse(ModbusFunctionCodes.ReadHoldingRegisters, 1, new RegisterCollection(1));
            Assert.AreEqual("Read 1 holding registers.", response.ToString());
        }

        [Test()]
        public void ReadInputRegistersResponse()
        {
            ReadHoldingInputRegistersResponse response = new ReadHoldingInputRegistersResponse(
                ModbusFunctionCodes.ReadInputRegisters, 5, new RegisterCollection(1, 2));
            Assert.AreEqual(ModbusFunctionCodes.ReadInputRegisters, response.FunctionCode);
            Assert.AreEqual(5, response.SlaveAddress);
            Assert.AreEqual(4, response.ByteCount);
            RegisterCollection col = new RegisterCollection(1, 2);
            Assert.AreEqual(col.NetworkBytes, response.Data.NetworkBytes);
        }

        [Test()]
        public void ToString_ReadInputRegistersResponse()
        {
            ReadHoldingInputRegistersResponse response = new ReadHoldingInputRegistersResponse(
                ModbusFunctionCodes.ReadInputRegisters, 1, new RegisterCollection(1));
            Assert.AreEqual("Read 1 input registers.", response.ToString());
        }
    }
}