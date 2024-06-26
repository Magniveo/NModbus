﻿using System;
using NModbus.Message;


namespace NModbus.UnitTests.Message
{
    public class WriteMultipleCoilsResponseFixture
    {
        [Test()]
        public void CreateWriteMultipleCoilsResponse()
        {
            WriteMultipleCoilsResponse response = new WriteMultipleCoilsResponse(17, 19, 45);
            Assert.AreEqual(ModbusFunctionCodes.WriteMultipleCoils, response.FunctionCode);
            Assert.AreEqual(17, response.SlaveAddress);
            Assert.AreEqual(19, response.StartAddress);
            Assert.AreEqual(45, response.NumberOfPoints);
        }

        [Test()]
        public void CreateWriteMultipleCoilsResponseTooMuchData()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new WriteMultipleCoilsResponse(1, 2, Modbus.MaximumDiscreteRequestResponseSize + 1));
        }

        [Test()]
        public void CreateWriteMultipleCoilsResponseMaxSize()
        {
            WriteMultipleCoilsResponse response = new WriteMultipleCoilsResponse(1, 2,
                Modbus.MaximumDiscreteRequestResponseSize);
            Assert.AreEqual(Modbus.MaximumDiscreteRequestResponseSize, response.NumberOfPoints);
        }

        [Test()]
        public void ToString_Test()
        {
            var response = new WriteMultipleCoilsResponse(1, 2, 3);

            Assert.AreEqual("Wrote 3 coils starting at address 2.", response.ToString());
        }
    }
}