namespace DataTransferLib.Models.Servers.Base
{
    public class HttpServer : TcpServer
    {
        public HttpServer(int port) : base(port)
        {
        }
    }
}
