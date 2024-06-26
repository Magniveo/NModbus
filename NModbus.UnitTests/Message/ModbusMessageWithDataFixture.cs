using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ModbusMessageWithDataFixture
    {
        [Test()]
        public void ModbusMessageWithDataFixtureCtorInitializesProperties()
        {
            AbstractModbusMessageWithData<DiscreteCollection> message = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 10, 1,
                new DiscreteCollection(true, false, true));
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, message.FunctionCode);
            Assert.AreEqual(10, message.SlaveAddress);
        }

        [Test()]
        public void ProtocolDataUnitReadCoilsResponse()
        {
            AbstractModbusMessageWithData<DiscreteCollection> message = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 1, 2,
                new DiscreteCollection(true));
            byte[] expectedResult = { 1, 2, 1 };
            Assert.AreEqual(expectedResult, message.ProtocolDataUnit);
        }

        [Test()]
        public void DataReadCoilsResponse()
        {
            DiscreteCollection col = new DiscreteCollection(false, true, false, true, false, true, false, false, false,
                false);
            AbstractModbusMessageWithData<DiscreteCollection> message = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 11, 1, col);
            Assert.AreEqual(col.Count, message.Data.Count);
            Assert.AreEqual(col.NetworkBytes, message.Data.NetworkBytes);
        }
    }
}