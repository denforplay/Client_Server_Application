using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Listeners;
using DataTransferLib.Models.Servers;
using Xunit;

namespace DataTransferTests.ModelsTests
{
    public sealed class TcpServerTests
    {
        [Fact]
        public void CreateTcpServerTest_ReturnsTrue()
        {
            IServer tcpServer = new TcpServer("127.0.0.1", 8080);
            Client client = new Client("127.0.0.1", 8080);
            Assert.True(tcpServer.IsStarted);
        }
    }
}
