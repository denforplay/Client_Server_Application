using DataTransferLib.Models.Listeners;
using System.Net.Sockets;
using System.Text;
using DataTransferLib.Models.Clients;
using System.Net;

namespace DataTransferLib.Models.Servers
{
    public class TcpServer : IServer
    {
        public delegate void ReceiveData(string message);
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
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            NetworkStream clientStream = client.GetStream();
            int bytes = 0;
            do
            {
                bytes = clientStream.Read(data, 0, data.Length);
                builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (clientStream.DataAvailable);

            OnDataReceived?.Invoke(builder.ToString());
            SendAnswer(clientStream, "Message" + "received");
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
