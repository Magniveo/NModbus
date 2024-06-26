using System.IO;
#if NET46
using System.Runtime.Serialization.Formatters.Binary;
#endif

namespace NModbus.UnitTests
{
    public class InvalidModbusRequestExceptionFixture
    {
        [Test ()]
        public void ConstructorWithExceptionCode()
        {
            var e = new InvalidModbusRequestException(SlaveExceptionCodes.SlaveDeviceBusy);
            Assert.AreEqual($"Modbus exception code {SlaveExceptionCodes.SlaveDeviceBusy}.", e.Message);
            Assert.AreEqual(SlaveExceptionCodes.SlaveDeviceBusy, e.ExceptionCode);
            Assert.Null(e.InnerException);
        }

        [Test ()]
        public void ConstructorWithExceptionCodeAndInnerException()
        {
            var inner = new IOException("Bar");
            var e = new InvalidModbusRequestException(42, inner);
            Assert.AreEqual("Modbus exception code 42.", e.Message);
            Assert.AreEqual(42, e.ExceptionCode);
            Assert.AreSame(inner, e.InnerException);
        }

        [Test ()]
        public void ConstructorWithMessageAndExceptionCode()
        {
            var e = new InvalidModbusRequestException("Hello World", SlaveExceptionCodes.IllegalFunction);
            Assert.AreEqual("Hello World", e.Message);
            Assert.AreEqual(SlaveExceptionCodes.IllegalFunction, e.ExceptionCode);
            Assert.Null(e.InnerException);
        }

        [Test ()]
        public void ConstructorWithCustomMessageAndSlaveExceptionResponse()
        {
            var inner = new IOException("Bar");
            var e = new InvalidModbusRequestException("Hello World", SlaveExceptionCodes.IllegalDataAddress, inner);
            Assert.AreEqual("Hello World", e.Message);
            Assert.AreEqual(SlaveExceptionCodes.IllegalDataAddress, e.ExceptionCode);
            Assert.AreSame(inner, e.InnerException);
        }

#if NET46
        [Test ()]
        public void Serializable()
        {
            var formatter = new BinaryFormatter();
            var e = new InvalidModbusRequestException(SlaveExceptionCodes.SlaveDeviceBusy);

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, e);
                stream.Position = 0;

                var e2 = (InvalidModbusRequestException)formatter.Deserialize(stream);
                Assert.NotNull(e2);
                Assert.AreEqual(SlaveExceptionCodes.SlaveDeviceBusy, e2.ExceptionCode);
                Assert.AreEqual($"Modbus exception code {SlaveExceptionCodes.SlaveDeviceBusy}.", e2.Message);
            }
        }
#endif
    }
}
