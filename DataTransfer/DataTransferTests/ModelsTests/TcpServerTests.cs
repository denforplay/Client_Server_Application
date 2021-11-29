using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Listeners;
using DataTransferLib.Models.Servers;
using System;
using Xunit;

namespace DataTransferTests.ModelsTests
{
    public sealed class TcpServerTests
    {
        [Theory]
        [InlineData("Hello")]
        public void CreateTcpServerTest_ReturnsTrue(string message)
        {
            TcpServer tcpServer = new TcpServer("127.0.0.1", 8080);
            tcpServer.OnDataReceived += (client, receivedMessage) =>
            {
                Assert.Equal(message, receivedMessage);
            };
            Client client = new Client("127.0.0.1", 8080);
            client.SendMessage(message);
          
        }
    }
}
