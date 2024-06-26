using NModbus.Data;
using NModbus.Message;

namespace NModbus.UnitTests.Message
{
    public class DiagnosticsRequestResponseFixture
    {
        [Test()]
        public void ToString_Test()
        {
            DiagnosticsRequestResponse response;

            response = new DiagnosticsRequestResponse(ModbusFunctionCodes.DiagnosticsReturnQueryData, 3, new RegisterCollection(5));
            Assert.AreEqual("Diagnostics message, sub-function return query data - {5}.", response.ToString());
        }
    }
}
