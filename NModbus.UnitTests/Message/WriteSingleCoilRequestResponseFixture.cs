using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class WriteSingleCoilRequestResponseFixture
    {
        [Test()]
        public void NewWriteSingleCoilRequestResponse()
        {
            WriteSingleCoilRequestResponse request = new WriteSingleCoilRequestResponse(11, 5, true);
            Assert.AreEqual(11, request.SlaveAddress);
            Assert.AreEqual(5, request.StartAddress);
            Assert.That(request.Data, Has.Exactly(1).Items);
            Assert.AreEqual(Modbus.CoilOn, request.Data[0]);
        }

        [Test()]
        public void ToString_True()
        {
            var request = new WriteSingleCoilRequestResponse(11, 5, true);

            Assert.AreEqual("Write single coil 1 at address 5.", request.ToString());
        }

        [Test()]
        public void ToString_False()
        {
            var request = new WriteSingleCoilRequestResponse(11, 5, false);

            Assert.AreEqual("Write single coil 0 at address 5.", request.ToString());
        }
    }
}
