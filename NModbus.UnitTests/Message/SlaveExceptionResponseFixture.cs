using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class SlaveExceptionResponseFixture
    {
        [Test()]
        public void CreateSlaveExceptionResponse()
        {
            SlaveExceptionResponse response = new SlaveExceptionResponse(11, ModbusFunctionCodes.ReadCoils + Modbus.ExceptionOffset,
                2);
            Assert.AreEqual(11, response.SlaveAddress);
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils + Modbus.ExceptionOffset, response.FunctionCode);
            Assert.AreEqual(2, response.SlaveExceptionCode);
        }

        [Test()]
        public void SlaveExceptionResponsePDU()
        {
            SlaveExceptionResponse response = new SlaveExceptionResponse(11, ModbusFunctionCodes.ReadCoils + Modbus.ExceptionOffset,
                2);
            Assert.AreEqual(new byte[] { response.FunctionCode, response.SlaveExceptionCode }, response.ProtocolDataUnit);
        }
    }
}