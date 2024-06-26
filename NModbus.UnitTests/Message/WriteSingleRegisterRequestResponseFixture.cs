using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class WriteSingleRegisterRequestResponseFixture
    {
        [Test()]
        public void NewWriteSingleRegisterRequestResponse()
        {
            WriteSingleRegisterRequestResponse message = new WriteSingleRegisterRequestResponse(12, 5, 1200);
            Assert.AreEqual(12, message.SlaveAddress);
            Assert.AreEqual(5, message.StartAddress);
            Assert.That(message.Data, Has.Exactly(1).Items);
            Assert.AreEqual(1200, message.Data[0]);
        }

        [Test()]
        public void ToStringOverride()
        {
            WriteSingleRegisterRequestResponse message = new WriteSingleRegisterRequestResponse(12, 5, 1200);
            Assert.AreEqual("Write single holding register 1200 at address 5.", message.ToString());
        }
    }
}
