using System.Net.Sockets;

namespace DataTransferLib.Models.Clients
{
    public class HttpClient : Client
    {
        public HttpClient(TcpClient client) : base(client)
        {
        }

        public HttpClient(string ip, int port) : base(ip, port)
        {
        }
    }
}
