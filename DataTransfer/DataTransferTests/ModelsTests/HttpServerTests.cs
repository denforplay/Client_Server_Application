using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Servers;
using DataTransferLib.Models.Servers.Base;
using System.Net.Http;
using Xunit;

namespace DataTransferTests.ModelsTests
{
    public sealed class HttpServerTests
    {
        [Fact]
        public void Test()
        {
            TcpServer server = new HttpServer("127.0.0.1", 8888);
        }
    }
}
