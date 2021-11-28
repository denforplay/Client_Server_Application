
using System.Net.Sockets;
using System.Text;

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

        public override void SendMessage(object content)
        {
            string message =
                "GET /echo HTTP/1.1\n"
                + $"Host: {_client.Client.RemoteEndPoint.AddressFamily}"
                + "Accept: text / html\n";

                content.ToString();
            byte[] data = Encoding.ASCII.GetBytes(message);
            GetStream().Write(data, 0, data.Length);
        }
    }
}
