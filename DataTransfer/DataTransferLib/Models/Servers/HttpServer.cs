using DataTransferLib.Models.Clients;
using System.Net;
using System.Net.Sockets;

namespace DataTransferLib.Models.Servers.Base
{
    public class HttpServer : TcpServer
    {
        public HttpServer(string ip, int port) : base(ip, port)
        {
        }

        public override void Start()
        {
            Listen();
        }

        protected override void SendAnswer(NetworkStream networkStream, string message)
        {
            message = "HTTP/1.1 200 OK\n" +
                      "Content-type: text/html\n" +
                      "Content-Length: " + message.Length + "\n\n"
                      + message;

            base.SendAnswer(networkStream, message);
        }
    }
}
