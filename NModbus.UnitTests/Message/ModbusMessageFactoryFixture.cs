using System;
using System.Linq;
using NModbus.Data;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ModbusMessageFactoryFixture
    {
        [Test()]
        public void CreateModbusMessageReadCoilsRequest()
        {
            ReadCoilsInputsRequest request =
                ModbusMessageFactory.CreateModbusMessage<ReadCoilsInputsRequest>(new byte[]
                { 11, ModbusFunctionCodes.ReadCoils, 0, 19, 0, 37 });
            ReadCoilsInputsRequest expectedRequest = new ReadCoilsInputsRequest(ModbusFunctionCodes.ReadCoils, 11, 19, 37);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(request, expectedRequest);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.NumberOfPoints, request.NumberOfPoints);
        }

        [Test()]
        public void CreateModbusMessageReadCoilsRequestWithInvalidFrameSize()
        {
            byte[] frame = { 11, ModbusFunctionCodes.ReadCoils, 4, 1, 2 };
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<ReadCoilsInputsRequest>(frame));
        }

        [Test()]
        public void CreateModbusMessageReadCoilsResponse()
        {
            ReadCoilsInputsResponse response =
                ModbusMessageFactory.CreateModbusMessage<ReadCoilsInputsResponse>(new byte[]
                { 11, ModbusFunctionCodes.ReadCoils, 1, 1 });
            ReadCoilsInputsResponse expectedResponse = new ReadCoilsInputsResponse(ModbusFunctionCodes.ReadCoils, 11, 1,
                new DiscreteCollection(true, false, false, false));
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedResponse, response);
            Assert.AreEqual(expectedResponse.Data.NetworkBytes, response.Data.NetworkBytes);
        }

        [Test()]
        public void CreateModbusMessageReadCoilsResponseWithNoByteCount()
        {
            byte[] frame = { 11, ModbusFunctionCodes.ReadCoils };
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<ReadCoilsInputsResponse>(frame));
        }

        [Test()]
        public void CreateModbusMessageReadCoilsResponseWithInvalidDataSize()
        {
            byte[] frame = { 11, ModbusFunctionCodes.ReadCoils, 4, 1, 2, 3 };
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<ReadCoilsInputsResponse>(frame));
        }

        [Test()]
        public void CreateModbusMessageReadHoldingRegistersRequest()
        {
            ReadHoldingInputRegistersRequest request =
                ModbusMessageFactory.CreateModbusMessage<ReadHoldingInputRegistersRequest>(new byte[]
                { 17, ModbusFunctionCodes.ReadHoldingRegisters, 0, 107, 0, 3 });
            ReadHoldingInputRegistersRequest expectedRequest =
                new ReadHoldingInputRegistersRequest(ModbusFunctionCodes.ReadHoldingRegisters, 17, 107, 3);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.NumberOfPoints, request.NumberOfPoints);
        }

        [Test()]
        public void CreateModbusMessageReadHoldingRegistersRequestWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<ReadHoldingInputRegistersRequest>(new byte[]
                { 11, ModbusFunctionCodes.ReadHoldingRegisters, 0, 0, 5 }));
        }

        [Test()]
        public void CreateModbusMessageReadHoldingRegistersResponse()
        {
            ReadHoldingInputRegistersResponse response =
                ModbusMessageFactory.CreateModbusMessage<ReadHoldingInputRegistersResponse>(new byte[]
                { 11, ModbusFunctionCodes.ReadHoldingRegisters, 4, 0, 3, 0, 4 });
            ReadHoldingInputRegistersResponse expectedResponse =
                new ReadHoldingInputRegistersResponse(ModbusFunctionCodes.ReadHoldingRegisters, 11, new RegisterCollection(3, 4));
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedResponse, response);
        }

        [Test()]
        public void CreateModbusMessageReadHoldingRegistersResponseWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<ReadHoldingInputRegistersResponse>(new byte[]
                { 11, ModbusFunctionCodes.ReadHoldingRegisters }));
        }

        [Test()]
        public void CreateModbusMessageSlaveExceptionResponse()
        {
            SlaveExceptionResponse response =
                ModbusMessageFactory.CreateModbusMessage<SlaveExceptionResponse>(new byte[] { 11, 129, 2 });
            SlaveExceptionResponse expectedException = new SlaveExceptionResponse(11,
                ModbusFunctionCodes.ReadCoils + Modbus.ExceptionOffset, 2);
            Assert.AreEqual(expectedException.FunctionCode, response.FunctionCode);
            Assert.AreEqual(expectedException.SlaveAddress, response.SlaveAddress);
            Assert.AreEqual(expectedException.MessageFrame, response.MessageFrame);
            Assert.AreEqual(expectedException.ProtocolDataUnit, response.ProtocolDataUnit);
        }

        [Test()]
        public void CreateModbusMessageSlaveExceptionResponseWithInvalidFunctionCode()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<SlaveExceptionResponse>(new byte[] { 11, 128, 2 }));
        }

        [Test()]
        public void CreateModbusMessageSlaveExceptionResponseWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<SlaveExceptionResponse>(new byte[] { 11, 128 }));
        }

        [Test()]
        public void CreateModbusMessageWriteSingleCoilRequestResponse()
        {
            WriteSingleCoilRequestResponse request =
                ModbusMessageFactory.CreateModbusMessage<WriteSingleCoilRequestResponse>(new byte[]
                { 17, ModbusFunctionCodes.WriteSingleCoil, 0, 172, byte.MaxValue, 0 });
            WriteSingleCoilRequestResponse expectedRequest = new WriteSingleCoilRequestResponse(17, 172, true);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.Data.NetworkBytes, request.Data.NetworkBytes);
        }

        [Test()]
        public void CreateModbusMessageWriteSingleCoilRequestResponseWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<WriteSingleCoilRequestResponse>(new byte[]
                { 11, ModbusFunctionCodes.WriteSingleCoil, 0, 105, byte.MaxValue }));
        }

        [Test()]
        public void CreateModbusMessageWriteSingleRegisterRequestResponse()
        {
            WriteSingleRegisterRequestResponse request =
                ModbusMessageFactory.CreateModbusMessage<WriteSingleRegisterRequestResponse>(new byte[]
                { 17, ModbusFunctionCodes.WriteSingleRegister, 0, 1, 0, 3 });
            WriteSingleRegisterRequestResponse expectedRequest = new WriteSingleRegisterRequestResponse(17, 1, 3);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.Data.NetworkBytes, request.Data.NetworkBytes);
        }

        [Test()]
        public void CreateModbusMessageWriteSingleRegisterRequestResponseWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<WriteSingleRegisterRequestResponse>(new byte[]
                { 11, ModbusFunctionCodes.WriteSingleRegister, 0, 1, 0 }));
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleRegistersRequest()
        {
            WriteMultipleRegistersRequest request =
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleRegistersRequest>(new byte[]
                { 11, ModbusFunctionCodes.WriteMultipleRegisters, 0, 5, 0, 1, 2, 255, 255 });
            WriteMultipleRegistersRequest expectedRequest = new WriteMultipleRegistersRequest(11, 5,
                new RegisterCollection(ushort.MaxValue));
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.NumberOfPoints, request.NumberOfPoints);
            Assert.AreEqual(expectedRequest.ByteCount, request.ByteCount);
            Assert.AreEqual(expectedRequest.Data.NetworkBytes, request.Data.NetworkBytes);
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleRegistersRequestWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleRegistersRequest>(new byte[]
                { 11, ModbusFunctionCodes.WriteMultipleRegisters, 0, 5, 0, 1, 2 }));
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleRegistersResponse()
        {
            WriteMultipleRegistersResponse response =
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleRegistersResponse>(new byte[]
                { 17, ModbusFunctionCodes.WriteMultipleRegisters, 0, 1, 0, 2 });
            WriteMultipleRegistersResponse expectedResponse = new WriteMultipleRegistersResponse(17, 1, 2);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedResponse, response);
            Assert.AreEqual(expectedResponse.StartAddress, response.StartAddress);
            Assert.AreEqual(expectedResponse.NumberOfPoints, response.NumberOfPoints);
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleCoilsRequest()
        {
            WriteMultipleCoilsRequest request =
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleCoilsRequest>(new byte[]
                { 17, ModbusFunctionCodes.WriteMultipleCoils, 0, 19, 0, 10, 2, 205, 1 });
            WriteMultipleCoilsRequest expectedRequest = new WriteMultipleCoilsRequest(17, 19,
                new DiscreteCollection(true, false, true, true, false, false, true, true, true, false));
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
            Assert.AreEqual(expectedRequest.StartAddress, request.StartAddress);
            Assert.AreEqual(expectedRequest.NumberOfPoints, request.NumberOfPoints);
            Assert.AreEqual(expectedRequest.ByteCount, request.ByteCount);
            Assert.AreEqual(expectedRequest.Data.NetworkBytes, request.Data.NetworkBytes);
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleCoilsRequestWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleCoilsRequest>(new byte[]
                { 17, ModbusFunctionCodes.WriteMultipleCoils, 0, 19, 0, 10, 2 }));
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleCoilsResponse()
        {
            WriteMultipleCoilsResponse response =
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleCoilsResponse>(new byte[]
                { 17, ModbusFunctionCodes.WriteMultipleCoils, 0, 19, 0, 10 });
            WriteMultipleCoilsResponse expectedResponse = new WriteMultipleCoilsResponse(17, 19, 10);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedResponse, response);
            Assert.AreEqual(expectedResponse.StartAddress, response.StartAddress);
            Assert.AreEqual(expectedResponse.NumberOfPoints, response.NumberOfPoints);
        }

        [Test()]
        public void CreateModbusMessageWriteMultipleCoilsResponseWithInvalidFrameSize()
        {
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<WriteMultipleCoilsResponse>(new byte[]
                { 17, ModbusFunctionCodes.WriteMultipleCoils, 0, 19, 0 }));
        }

        //TODO: Bring this test back from the dead.
        //[Test()]
        //public void CreateModbusMessageReadWriteMultipleRegistersRequest()
        //{
        //    ReadWriteMultipleRegistersRequest request =
        //        ModbusMessageFactory.CreateModbusMessage<ReadWriteMultipleRegistersRequest>(new byte[]
        //        { 0x05, 0x17, 0x00, 0x03, 0x00, 0x06, 0x00, 0x0e, 0x00, 0x03, 0x06, 0x00, 0xff, 0x00, 0xff, 0x00, 0xff });
        //    RegisterCollection writeCollection = new RegisterCollection(255, 255, 255);
        //    ReadWriteMultipleRegistersRequest expectedRequest = new ReadWriteMultipleRegistersRequest(5, 3, 6, 14,
        //        writeCollection);
        //    ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
        //}

        [Test()]
        public void CreateModbusMessageReadWriteMultipleRegistersRequestWithInvalidFrameSize()
        {
            byte[] frame = { 17, ModbusFunctionCodes.ReadWriteMultipleRegisters, 1, 2, 3 };
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<ReadWriteMultipleRegistersRequest>(frame));
        }

        [Test()]
        public void CreateModbusMessageReturnQueryDataRequestResponse()
        {
            const byte slaveAddress = 5;
            RegisterCollection data = new RegisterCollection(50);
            byte[] frame = new byte[] { slaveAddress, 8, 0, 0 }.Concat(data.NetworkBytes).ToArray();
            DiagnosticsRequestResponse message =
                ModbusMessageFactory.CreateModbusMessage<DiagnosticsRequestResponse>(frame);
            DiagnosticsRequestResponse expectedMessage =
                new DiagnosticsRequestResponse(ModbusFunctionCodes.DiagnosticsReturnQueryData, slaveAddress, data);

            Assert.AreEqual(expectedMessage.SubFunctionCode, message.SubFunctionCode);
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedMessage, message);
        }

        [Test()]
        public void CreateModbusMessageReturnQueryDataRequestResponseTooSmall()
        {
            byte[] frame = new byte[] { 5, 8, 0, 0, 5 };
            Assert.Throws<FormatException>(() =>
                ModbusMessageFactory.CreateModbusMessage<DiagnosticsRequestResponse>(frame));
        }

        [Test()]
        public void CreateModbusMessageWriteFileRecordRequest()
        {
            var request =
                ModbusMessageFactory.CreateModbusMessage<WriteFileRecordRequest>(new byte[]
                { 17, ModbusFunctionCodes.WriteFileRecord, 9, 6, 0, 1, 0, 2, 0, 1, 1, 2 });
            var expectedRequest = new WriteFileRecordRequest(17, new FileRecordCollection(1, 2, new byte[] { 1, 2 }));
            ModbusMessageFixture.AssertModbusMessagePropertiesAreEqual(expectedRequest, request);
        }

        [Test()]
        public void CreateModbusMessageWriteFileRecordRequestThrowsOnNotEnoughBytes()
        {
            Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusMessage<WriteFileRecordRequest>(new byte[]
                { 17, ModbusFunctionCodes.WriteFileRecord, 0, 19, 0, 10 }));
        }

        //[Test()]
        //public void CreateModbusRequestWithInvalidMessageFrame()
        //{
        //    Assert.Throws<FormatException>(() => ModbusMessageFactory.CreateModbusRequest(new byte[] { 0, 1 }));
        //}

        //[Test()]
        //public void CreateModbusRequestWithInvalidFunctionCode()
        //{
        //    Assert.Throws<ArgumentException>(() => ModbusMessageFactory.CreateModbusRequest(new byte[] { 1, 99, 0, 0, 0, 1, 23 }));
        //}

        //[Test()]
        //public void CreateModbusRequestForReadCoils()
        //{
        //    ReadCoilsInputsRequest req = new ReadCoilsInputsRequest(1, 2, 1, 10);
        //    IModbusMessage request = ModbusMessageFactory.CreateModbusRequest(req.MessageFrame);
        //    Assert.AreEqual(typeof(ReadCoilsInputsRequest), request.GetType());
        //}

        //[Test()]
        //public void CreateModbusRequestForDiagnostics()
        //{
        //    DiagnosticsRequestResponse diagnosticsRequest = new DiagnosticsRequestResponse(0, 2,
        //        new RegisterCollection(45));
        //    IModbusMessage request = ModbusMessageFactory.CreateModbusRequest(diagnosticsRequest.MessageFrame);
        //    Assert.AreEqual(typeof(DiagnosticsRequestResponse), request.GetType());
        //}
    }
}