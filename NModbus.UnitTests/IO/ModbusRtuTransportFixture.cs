using System;
using System.IO;
using System.Linq;
using Moq;
using NModbus.Data;
using NModbus.IO;
using NModbus.Logging;
using NModbus.Message;
using NModbus.Utility;


namespace NModbus.UnitTests.IO
{
    public class ModbusRtuTransportFixture
    {
        private static IStreamResource StreamResource => new Mock<IStreamResource>(MockBehavior.Strict).Object;
        private static IModbusFactory Factory = new ModbusFactory();

        [Test()]
        public void BuildMessageFrame()
        {
            byte[] message = { 17, ModbusFunctionCodes.ReadCoils, 0, 19, 0, 37, 14, 132 };
            var request = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 17, 19, 37);
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);

            Assert.AreEqual(message, transport.BuildMessageFrame(request));
        }

        [Test()]
        public void ResponseBytesToReadCoils()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x01, 0x05, 0xCD, 0x6B, 0xB2, 0x0E, 0x1B };
            Assert.AreEqual(6, transport.ResponseBytesToRead(frameStart));
        }

        [Test()]
        public void ResponseBytesToReadCoilsNoData()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x01, 0x00, 0x00, 0x00 };
            Assert.AreEqual(1, transport.ResponseBytesToRead(frameStart));
        }

        [Test()]
        public void ResponseBytesToReadWriteCoilsResponse()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x0F, 0x00, 0x13, 0x00, 0x0A, 0, 0 };
            Assert.AreEqual(4, transport.ResponseBytesToRead(frameStart));
        }

        [Test()]
        public void ResponseBytesToReadDiagnostics()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x01, 0x08, 0x00, 0x00 };
            Assert.AreEqual(4, transport.ResponseBytesToRead(frameStart));
        }

        [Test()]
        public void ResponseBytesToReadSlaveException()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x01, Modbus.ExceptionOffset + 1, 0x01 };
            Assert.AreEqual(1, transport.ResponseBytesToRead(frameStart));
        }

        [Test()]
        public void ResponseBytesToReadInvalidFunctionCode()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frame = { 0x11, 0x16, 0x00, 0x01, 0x00, 0x02, 0x04 };
            Assert.Throws<NotImplementedException>(() => transport.ResponseBytesToRead(frame));
        }

        [Test()]
        public void RequestBytesToReadDiagnostics()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frame = { 0x01, 0x08, 0x00, 0x00, 0xA5, 0x37, 0, 0 };
            Assert.AreEqual(1, transport.RequestBytesToRead(frame));
        }

        [Test()]
        public void RequestBytesToReadCoils()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x01, 0x00, 0x13, 0x00, 0x25 };
            Assert.AreEqual(1, transport.RequestBytesToRead(frameStart));
        }

        [Test()]
        public void RequestBytesToReadWriteCoilsRequest()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x0F, 0x00, 0x13, 0x00, 0x0A, 0x02, 0xCD, 0x01 };
            Assert.AreEqual(4, transport.RequestBytesToRead(frameStart));
        }

        [Test()]
        public void RequestBytesToReadWriteMultipleHoldingRegisters()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frameStart = { 0x11, 0x10, 0x00, 0x01, 0x00, 0x02, 0x04 };
            Assert.AreEqual(6, transport.RequestBytesToRead(frameStart));
        }

        [Test()]
        public void RequestBytesToReadInvalidFunctionCode()
        {
            var transport = new ModbusRtuTransport(StreamResource, Factory, NullModbusLogger.Instance);
            byte[] frame = { 0x11, 0xFF, 0x00, 0x01, 0x00, 0x02, 0x04 };
            Assert.Throws<NotImplementedException>(() => transport.RequestBytesToRead(frame));
        }

        [Test()]
        public void ChecksumsMatchSucceed()
        {
            var factory = new ModbusFactory();
            var transport = new ModbusRtuTransport(StreamResource, factory, NullModbusLogger.Instance);
            var message = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 17, 19, 37);
            byte[] frame = { 17, ModbusFunctionCodes.ReadCoils, 0, 19, 0, 37, 14, 132 };

            Assert.True(transport.ChecksumsMatch(message, frame));
        }

        [Test()]
        public void ChecksumsMatchFail()
        {
            var factory = new ModbusFactory();
            var transport = new ModbusRtuTransport(StreamResource, factory, NullModbusLogger.Instance);
            var message = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 17, 19, 38);
            byte[] frame = { 17, ModbusFunctionCodes.ReadCoils, 0, 19, 0, 37, 14, 132 };

            Assert.False(transport.ChecksumsMatch(message, frame));
        }

        [Test()]
        public void ReadResponse()
        {
            var factory = new ModbusFactory();
            var mock = new Mock<ModbusRtuTransport>(StreamResource, factory, NullModbusLogger.Instance) { CallBase = true };
            var transport = mock.Object;

            mock.Setup(t => t.Read(ModbusRtuTransport.ResponseFrameStartLength)).Returns(new byte[] { 1, 1, 1, 0 });
            mock.Setup(t => t.Read(2)).Returns(new byte[] { 81, 136 });

            var response = transport.ReadResponse<ReadCoilsInputsResponse>();
            Assert.IsInstanceOf<ReadCoilsInputsResponse>(response);

            var expectedResponse = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 1, 1, new DiscreteCollection(false));
            Assert.AreEqual(expectedResponse.MessageFrame, response.MessageFrame);

            mock.VerifyAll();
        }

        [Test()]
        public void ReadResponseSlaveException()
        {
            var factory = new ModbusFactory();
            var mock = new Mock<ModbusRtuTransport>(StreamResource, factory, NullModbusLogger.Instance) { CallBase = true };
            var transport = mock.Object;

            byte[] messageFrame = { 0x01, 0x81, 0x02 };
            byte[] crc = ModbusUtility.CalculateCrc(messageFrame);

            mock.Setup(t => t.Read(ModbusRtuTransport.ResponseFrameStartLength))
                .Returns(Enumerable.Concat(messageFrame, new byte[] { crc[0] }).ToArray());

            mock.Setup(t => t.Read(1))
                .Returns(new byte[] { crc[1] });

            var response = transport.ReadResponse<ReadCoilsInputsResponse>();
            Assert.IsInstanceOf<SlaveExceptionResponse>(response);

            var expectedResponse = new SlaveExceptionResponse(0x01, 0x81, 0x02);
            Assert.AreEqual(expectedResponse.MessageFrame, response.MessageFrame);

            mock.VerifyAll();
        }

        /// <summary>
        /// We want to throw an IOException for any message w/ an invalid checksum,
        /// this must preceed throwing a SlaveException based on function code > 127
        /// </summary>
        [Test()]
        public void ReadResponseSlaveExceptionWithErroneousLrc()
        {
            var factory = new ModbusFactory();
            var mock = new Mock<ModbusRtuTransport>(StreamResource, factory, NullModbusLogger.Instance) { CallBase = true };
            var transport = mock.Object;

            byte[] messageFrame = { 0x01, 0x81, 0x02 };

            // invalid crc
            byte[] crc = { 0x9, 0x9 };

            mock.Setup(t => t.Read(ModbusRtuTransport.ResponseFrameStartLength))
                .Returns(Enumerable.Concat(messageFrame, new byte[] { crc[0] }).ToArray());

            mock.Setup(t => t.Read(1))
                .Returns(new byte[] { crc[1] });

            Assert.Throws<IOException>(() => transport.ReadResponse<ReadCoilsInputsResponse>());

            mock.VerifyAll();
        }

        [Test()]
        public void ReadRequest()
        {
            var factory = new ModbusFactory();
            var mock = new Mock<ModbusRtuTransport>(StreamResource, factory, NullModbusLogger.Instance) { CallBase = true };
            var transport = mock.Object;

            mock.Setup(t => t.Read(ModbusRtuTransport.RequestFrameStartLength))
                .Returns(new byte[] { 1, 1, 1, 0, 1, 0, 0 });

            mock.Setup(t => t.Read(1))
                .Returns(new byte[] { 5 });

            Assert.AreEqual(new byte[] { 1, 1, 1, 0, 1, 0, 0, 5 }, transport.ReadRequest());

            mock.VerifyAll();
        }

        [Test()]
        public void Read()
        {
            var mock = new Mock<IStreamResource>(MockBehavior.Strict);

            mock.Setup(s => s.Read(It.Is<byte[]>(x => x.Length == 5), 0, 5))
                .Returns((byte[] buf, int offset, int count) =>
                {
                    Array.Copy(new byte[] { 2, 2, 2 }, buf, 3);
                    return 3;
                });

            mock.Setup(s => s.Read(It.Is<byte[]>(x => x.Length == 5), 3, 2))
                .Returns((byte[] buf, int offset, int count) =>
                {
                    Array.Copy(new byte[] { 3, 3 }, 0, buf, 3, 2);
                    return 2;
                });

            var factory = new ModbusFactory();

            ModbusRtuTransport transport = new ModbusRtuTransport(mock.Object, factory, NullModbusLogger.Instance);
            Assert.AreEqual(new byte[] { 2, 2, 2, 3, 3 }, transport.Read(5));

            mock.VerifyAll();
        }
    }
}
