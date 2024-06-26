using NModbus.Device.MessageHandlers;


namespace NModbus.UnitTests.Device.MessageHandlers
{
    public class WriteFileRecordServiceFixture
    {
        [Test()]
        public void GetRtuRequestBytesToRead()
        {
            var service = new WriteFileRecordService();
            Assert.AreEqual(4, service.GetRtuRequestBytesToRead(new byte[] { 1, 21, 3 }));
        }

        [Test()]
        public void GetRtuResponseBytesToRead()
        {
            var service = new WriteFileRecordService();
            Assert.AreEqual(45, service.GetRtuResponseBytesToRead(new byte[] { 1, 21, 44 }));
        }
    }
}
