using DataTransferLib.Models.Listeners;
using System.Net.Sockets;
using System.Text;
using DataTransferLib.Models.Clients;
using System.Net;
using DataTransferLib.Models.Clients.Base;

namespace DataTransferLib.Models.Servers
{
    public class TcpServer : IServer
    {
        public delegate void ReceiveData(IClient client, string message);
        public event ReceiveData OnDataReceived;

        private TcpListener _server;
        private Thread _listenThread;

        public bool IsStarted { get; set; }

        public TcpServer(string ip, int port)
        {
            if (IPAddress.TryParse(ip, out var serverIp))
            {
                _server = new TcpListener(serverIp, port);
            }

            Start();
        }

        public virtual void Start()
        {
            _listenThread = new Thread(Listen);
            _listenThread.Start();
        }

        public virtual void Listen()
        {
            _server.Start();
            IsStarted = true;
            while (IsStarted)
            {
                try
                {
                    Client client = new Client(_server.AcceptTcpClient());
                    ReceiveMessage(client);
                }
                catch
                {
                    break;
                }
            }
        }

        protected virtual void ReceiveMessage(IClient client)
        {
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[64];
            client.GetStream().Read(data);
            builder.AppendLine(Encoding.UTF8.GetString(data));
            OnDataReceived?.Invoke(client, builder.ToString());
            SendAnswer(client, "Message" + "received" + builder.ToString());
            client.Stop();
        }

        protected virtual void SendAnswer(IClient client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.GetStream().Write(data, 0, data.Length);
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
