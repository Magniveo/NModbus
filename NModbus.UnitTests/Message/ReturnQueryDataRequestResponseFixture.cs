using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ReturnQueryDataRequestResponseFixture
    {
        [Test()]
        public void ReturnQueryDataRequestResponse()
        {
            RegisterCollection data = new RegisterCollection(1, 2, 3, 4);
            DiagnosticsRequestResponse request = new DiagnosticsRequestResponse(ModbusFunctionCodes.DiagnosticsReturnQueryData, 5,
                data);
            Assert.AreEqual(ModbusFunctionCodes.Diagnostics, request.FunctionCode);
            Assert.AreEqual(ModbusFunctionCodes.DiagnosticsReturnQueryData, request.SubFunctionCode);
            Assert.AreEqual(5, request.SlaveAddress);
            Assert.AreEqual(data.NetworkBytes, request.Data.NetworkBytes);
        }

        [Test()]
        public void ProtocolDataUnit()
        {
            RegisterCollection data = new RegisterCollection(1, 2, 3, 4);
            DiagnosticsRequestResponse request = new DiagnosticsRequestResponse(ModbusFunctionCodes.DiagnosticsReturnQueryData, 5,
                data);
            Assert.AreEqual(new byte[] { 8, 0, 0, 0, 1, 0, 2, 0, 3, 0, 4 }, request.ProtocolDataUnit);
        }
    }
}