using System;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadHoldingInputRegistersRequestFixture
    {
        [Test()]
        public void CreateReadHoldingRegistersRequest()
        {
            ReadHoldingInputRegistersRequest request = new ReadHoldingInputRegistersRequest(
                ModbusFunctionCodes.ReadHoldingRegisters, 5, 1, 10);
            Assert.AreEqual(ModbusFunctionCodes.ReadHoldingRegisters, request.FunctionCode);
            Assert.AreEqual(5, request.SlaveAddress);
            Assert.AreEqual(1, request.StartAddress);
            Assert.AreEqual(10, request.NumberOfPoints);
        }

        [Test()]
        public void CreateReadInputRegistersRequest()
        {
            ReadHoldingInputRegistersRequest request = new ReadHoldingInputRegistersRequest(ModbusFunctionCodes.ReadInputRegisters, 5,
                1, 10);
            Assert.AreEqual(ModbusFunctionCodes.ReadInputRegisters, request.FunctionCode);
            Assert.AreEqual(5, request.SlaveAddress);
            Assert.AreEqual(1, request.StartAddress);
            Assert.AreEqual(10, request.NumberOfPoints);
        }

        [Test()]
        public void CreateReadHoldingInputRegistersRequestTooMuchData()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReadHoldingInputRegistersRequest(ModbusFunctionCodes.ReadHoldingRegisters, 1, 2,
                Modbus.MaximumRegisterRequestResponseSize + 1));
        }

        [Test()]
        public void CreateReadHoldingInputRegistersRequestMaxSize()
        {
            ReadHoldingInputRegistersRequest response = new ReadHoldingInputRegistersRequest(
                ModbusFunctionCodes.ReadHoldingRegisters, 1, 2, Modbus.MaximumRegisterRequestResponseSize);
            Assert.AreEqual(Modbus.MaximumRegisterRequestResponseSize, response.NumberOfPoints);
        }

        [Test()]
        public void ToString_ReadHoldingRegistersRequest()
        {
            ReadHoldingInputRegistersRequest request = new ReadHoldingInputRegistersRequest(
                ModbusFunctionCodes.ReadHoldingRegisters, 5, 1, 10);

            Assert.AreEqual("Read 10 holding registers starting at address 1.", request.ToString());
        }

        [Test()]
        public void ToString_ReadInputRegistersRequest()
        {
            ReadHoldingInputRegistersRequest request = new ReadHoldingInputRegistersRequest(ModbusFunctionCodes.ReadInputRegisters, 5,
                1, 10);

            Assert.AreEqual("Read 10 input registers starting at address 1.", request.ToString());
        }
    }
}