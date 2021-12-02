using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Listeners;
using DataTransferLib.Models.Servers;
using System;
using Xunit;

namespace DataTransferTests.ModelsTests
{
    public sealed class TcpServerTests
    {
        [Fact]
        public void CreateTcpServer_NullOrEmptyIp_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TcpServer("", 8080));
        }

        [Fact]
        public void CreateTcpServer_InvalidIp_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TcpServer("256.256.256.256", 8080));
        }

        [Theory]
        [InlineData("Hello")]
        public void CreateTcpServerTest_ReturnsTrue(string message)
        {
            TcpServer tcpServer = new TcpServer("127.0.0.1", 8080);
            tcpServer.DataReceiveEvent += (client, receivedMessage) =>
            {
                Assert.Equal(message, receivedMessage);
            };
            Client client = new Client("127.0.0.1", 8080);
            client.SendMessage(message);
          
        }
    }
}
