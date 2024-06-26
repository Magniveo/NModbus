using System;
using NModbus.Device;


namespace NModbus.UnitTests.Device
{
    public class TcpConnectionEventArgsFixture
    {
        [Test()]
        public void TcpConnectionEventArgs_NullEndPoint()
        {
            Assert.Throws<ArgumentNullException>(() => new TcpConnectionEventArgs(null));
        }

        [Test()]
        public void TcpConnectionEventArgs_EmptyEndPoint()
        {
            Assert.Throws<ArgumentException>(() => new TcpConnectionEventArgs(string.Empty));
        }

        [Test()]
        public void TcpConnectionEventArgs()
        {
            var args = new TcpConnectionEventArgs("foo");

            Assert.AreEqual("foo", args.EndPoint);
        }
    }
}