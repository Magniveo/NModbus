using System;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class ModbusMessageImplFixture
    {
        [Test()]
        public void ModbusMessageCtorInitializesProperties()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl(5, ModbusFunctionCodes.ReadCoils);
            Assert.AreEqual(5, messageImpl.SlaveAddress);
            Assert.AreEqual(ModbusFunctionCodes.ReadCoils, messageImpl.FunctionCode);
        }

        [Test()]
        public void Initialize()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl();
            messageImpl.Initialize(new byte[] { 1, 2, 9, 9, 9, 9 });
            Assert.AreEqual(1, messageImpl.SlaveAddress);
            Assert.AreEqual(2, messageImpl.FunctionCode);
        }

        [Test()]
        public void ChecckInitializeFrameNull()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl();
            Assert.Throws<ArgumentNullException>(() => messageImpl.Initialize(null));
        }

        [Test()]
        public void InitializeInvalidFrame()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl();
            Assert.Throws<FormatException>(() => messageImpl.Initialize(new byte[] { 1 }));
        }

        [Test()]
        public void ProtocolDataUnit()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl(11, ModbusFunctionCodes.ReadCoils);
            byte[] expectedResult = { ModbusFunctionCodes.ReadCoils };
            Assert.AreEqual(expectedResult, messageImpl.ProtocolDataUnit);
        }

        [Test()]
        public void MessageFrame()
        {
            ModbusMessageImpl messageImpl = new ModbusMessageImpl(11, ModbusFunctionCodes.ReadHoldingRegisters);
            byte[] expectedMessageFrame = { 11, ModbusFunctionCodes.ReadHoldingRegisters };
            Assert.AreEqual(expectedMessageFrame, messageImpl.MessageFrame);
        }
    }
}