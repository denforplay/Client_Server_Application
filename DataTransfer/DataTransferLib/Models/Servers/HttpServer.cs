using DataTransferLib.Models.Clients;
using System.Net;
using System.Net.Sockets;

namespace DataTransferLib.Models.Servers.Base
{
    public class HttpServer : TcpServer
    {
        public static string VERSION = "HTTP/1.1";

        public HttpServer(string ip, int port) : base(ip, port)
        {
        }

        public override void Start()
        {
            OnDataReceived += (client, message) =>
            {
                SendAnswer(client.GetStream(), "Message" + "received" + message);
                client.Stop();
            };

            Listen();
        }

        protected override void SendAnswer(NetworkStream networkStream, string message)
        {
            Request request = Request.GetRequest(message);
            Response response = Response.From(request);
            response.Post(networkStream);
        }
    }
}
