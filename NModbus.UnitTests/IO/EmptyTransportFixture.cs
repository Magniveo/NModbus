﻿using System;
using NModbus.IO;
using NModbus.Message;


namespace NModbus.UnitTests.IO
{
    public static class EmptyTransportFixture
    {
        [Test()]
        public static void Negative()
        {
            var transport = new EmptyTransport(new ModbusFactory());
            Assert.Throws<NotImplementedException>(() => transport.ReadRequest());
            Assert.Throws<NotImplementedException>(() => transport.ReadResponse<ReadCoilsInputsResponse>());
            Assert.Throws<NotImplementedException>(() => transport.BuildMessageFrame(null));
            Assert.Throws<NotImplementedException>(() => transport.Write(null));
            Assert.Throws<NotImplementedException>(() => transport.OnValidateResponse(null, null));
        }
    }
}
