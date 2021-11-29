using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Clients.Base;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
            Listen();
        }

        protected override void ReceiveMessage(IClient client)
        {
            StringBuilder builder = new StringBuilder();
            StreamReader reader = new StreamReader(client.GetStream());
            while (reader.Peek() != -1)
            {
                builder.AppendLine(reader.ReadLine());
            }
            //OnDataReceived?.Invoke(client, builder.ToString());

            SendAnswer(client, "Message" + "received" + builder.ToString());
            client.Stop();
            Stop();
        }

        protected override void SendAnswer(IClient client, string message)
        {
            Request request = Request.GetRequest(message);
            Response response = Response.From(request);
            response.Post(client.GetStream());
        }
    }
}
