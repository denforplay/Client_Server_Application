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

        private void ReceiveMessage(IClient client)
        {
            StringBuilder builder = new StringBuilder();
            StreamReader reader = new StreamReader(client.GetStream());
            while (reader.Peek() != -1)
            {
                builder.AppendLine(reader.ReadLine());
            }

            OnDataReceived?.Invoke(client, builder.ToString());
           
            SendAnswer(client.GetStream(), "Message" + "received" + builder.ToString());
            client.Stop();
        }

        protected virtual void SendAnswer(NetworkStream networkStream, string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            networkStream.Write(data, 0, data.Length);
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
