using DataTransferLib.Models.Clients;
using DataTransferLib.Models.Clients.Base;
using System.Text;
using HttpClient = DataTransferLib.Models.Clients.HttpClient;

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

        public override void Listen()
        {
            _server.Start();
            IsStarted = true;
            while (IsStarted)
            {
                try
                {
                    IClient client = new HttpClient(_server.AcceptTcpClient());
                    ReceiveMessage(client);
                }
                catch
                {
                    break;
                }
            }
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
        }

        protected override void SendAnswer(IClient client, string message)
        {
            Request request = Request.GetRequest(message);
            Response response = Response.From(request);
            response.Post(client.GetStream());
        }
    }
}
