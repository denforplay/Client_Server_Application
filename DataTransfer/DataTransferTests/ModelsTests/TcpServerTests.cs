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
            IServer tcpServer = new TcpServer(8080);
            Assert.True(tcpServer.IsConnected);
        }
    }
}
