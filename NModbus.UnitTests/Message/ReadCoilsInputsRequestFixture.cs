using System;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadCoilsInputsRequestFixture
    {
        [Test()]
        public void CreateReadCoilsRequest()
        {
            ReadCoilsInputsRequest request = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 5, 1, 10);
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, request.FunctionCode);
            Assert.AreEqual(5, request.SlaveAddress);
            Assert.AreEqual(1, request.StartAddress);
            Assert.AreEqual(10, request.NumberOfPoints);
        }

        [Test()]
        public void CreateReadInputsRequest()
        {
            ReadCoilsInputsRequest request = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadInputs, 5, 1, 10);
            Assert.AreEqual(ModbusFunctionCodes.ReadInputs, request.FunctionCode);
            Assert.AreEqual(5, request.SlaveAddress);
            Assert.AreEqual(1, request.StartAddress);
            Assert.AreEqual(10, request.NumberOfPoints);
        }

        [Test()]
        public void CreateReadCoilsInputsRequestTooMuchData()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 1, 2, Modbus.MaximumDiscreteRequestResponseSize + 1));
        }

        [Test()]
        public void CreateReadCoilsInputsRequestMaxSize()
        {
            ReadCoilsInputsRequest response = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 1, 2,
                Modbus.MaximumDiscreteRequestResponseSize);
            Assert.AreEqual(Modbus.MaximumDiscreteRequestResponseSize, response.NumberOfPoints);
        }

        [Test()]
        public void ToString_ReadCoilsRequest()
        {
            ReadCoilsInputsRequest request = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 5, 1, 10);

            Assert.AreEqual("Read 10 coils starting at address 1.", request.ToString());
        }

        [Test()]
        public void ToString_ReadInputsRequest()
        {
            ReadCoilsInputsRequest request = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadInputs, 5, 1, 10);

            Assert.AreEqual("Read 10 inputs starting at address 1.", request.ToString());
        }
    }
}