using System;
using System.Globalization;
using System.IO;
using System.Threading;
using NModbus.Message;
#if NET46
using System.Runtime.Serialization.Formatters.Binary;
#endif

namespace NModbus.UnitTests
{
    public class SlaveExceptionFixture
    {
        [Test ()]
        public void EmptyConstructor()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var e = new SlaveException();
            Assert.AreEqual($"Exception of type '{typeof(SlaveException).FullName}' was thrown.", e.Message);
            Assert.AreEqual(0, e.SlaveAddress);
            Assert.AreEqual(0, e.FunctionCode);
            Assert.AreEqual(0, e.SlaveExceptionCode);
            Assert.Null(e.InnerException);
        }

        [Test ()]
        public void ConstructorWithMessage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var e = new SlaveException("Hello World");
            Assert.AreEqual("Hello World", e.Message);
            Assert.AreEqual(0, e.SlaveAddress);
            Assert.AreEqual(0, e.FunctionCode);
            Assert.AreEqual(0, e.SlaveExceptionCode);
            Assert.Null(e.InnerException);
        }

        [Test ()]
        public void ConstructorWithMessageAndInnerException()
        {
            var inner = new IOException("Bar");
            var e = new SlaveException("Foo", inner);
            Assert.AreEqual("Foo", e.Message);
            Assert.AreSame(inner, e.InnerException);
            Assert.AreEqual(0, e.SlaveAddress);
            Assert.AreEqual(0, e.FunctionCode);
            Assert.AreEqual(0, e.SlaveExceptionCode);
        }

        [Test ()]
        public void ConstructorWithSlaveExceptionResponse()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var response = new SlaveExceptionResponse(12, ModbusFunctionCodes.ReadCoils, 1);
            var e = new SlaveException(response);

            Assert.AreEqual(12, e.SlaveAddress);
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, e.FunctionCode);
            Assert.AreEqual(1, e.SlaveExceptionCode);
            Assert.Null(e.InnerException);

            Assert.AreEqual(
                $@"Exception of type '{typeof(SlaveException).FullName}' was thrown.{Environment.NewLine}Function Code: {response.FunctionCode}{Environment.NewLine}Exception Code: {response.SlaveExceptionCode} - {Resources.IllegalFunction}",
                e.Message);
        }

        [Test ()]
        public void ConstructorWithCustomMessageAndSlaveExceptionResponse()
        {
            var response = new SlaveExceptionResponse(12, ModbusFunctionCodes.ReadCoils, 2);
            string customMessage = "custom message";
            var e = new SlaveException(customMessage, response);

            Assert.AreEqual(12, e.SlaveAddress);
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, e.FunctionCode);
            Assert.AreEqual(2, e.SlaveExceptionCode);
            Assert.Null(e.InnerException);

            Assert.AreEqual(
                $@"{customMessage}{Environment.NewLine}Function Code: {response.FunctionCode}{Environment.NewLine}Exception Code: {response.SlaveExceptionCode} - {Resources.IllegalDataAddress}",
                e.Message);
        }

#if NET46
        [Test ()]
        public void Serializable()
        {
            var formatter = new BinaryFormatter();
            var e = new SlaveException(new SlaveExceptionResponse(1, 2, 3));

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, e);
                stream.Position = 0;

                var e2 = (SlaveException)formatter.Deserialize(stream);
                Assert.NotNull(e2);
                Assert.AreEqual(1, e2.SlaveAddress);
                Assert.AreEqual(2, e2.FunctionCode);
                Assert.AreEqual(3, e2.SlaveExceptionCode);
            }
        }
#endif
    }
}
