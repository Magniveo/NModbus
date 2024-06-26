using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReadCoilsInputsResponseFixture
    {
        [Test()]
        public void CreateReadCoilsResponse()
        {
            ReadCoilsInputsResponse response = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 5, 2,
                new DiscreteCollection(true, true, true, true, true, true, false, false, true, true, false));
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, response.FunctionCode);
            Assert.AreEqual(5, response.SlaveAddress);
            Assert.AreEqual(2, response.ByteCount);
            DiscreteCollection col = new DiscreteCollection(true, true, true, true, true, true, false, false, true, true,
                false);
            Assert.AreEqual(col.NetworkBytes, response.Data.NetworkBytes);
        }

        [Test()]
        public void CreateReadInputsResponse()
        {
            ReadCoilsInputsResponse response = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadInputs, 5, 2,
                new DiscreteCollection(true, true, true, true, true, true, false, false, true, true, false));
            Assert.AreEqual(ModbusFunctionCodes.ReadInputs, response.FunctionCode);
            Assert.AreEqual(5, response.SlaveAddress);
            Assert.AreEqual(2, response.ByteCount);
            DiscreteCollection col = new DiscreteCollection(true, true, true, true, true, true, false, false, true, true,
                false);
            Assert.AreEqual(col.NetworkBytes, response.Data.NetworkBytes);
        }

        [Test()]
        public void ToString_Coils()
        {
            ReadCoilsInputsResponse response = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 5, 2,
                new DiscreteCollection(true, true, true, true, true, true, false, false, true, true, false));

            Assert.AreEqual("Read 11 coils - {1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0}.", response.ToString());
        }

        [Test()]
        public void ToString_Inputs()
        {
            ReadCoilsInputsResponse response = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadInputs, 5, 2,
                new DiscreteCollection(true, true, true, true, true, true, false, false, true, true, false));

            Assert.AreEqual("Read 11 inputs - {1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0}.", response.ToString());
        }
    }
}