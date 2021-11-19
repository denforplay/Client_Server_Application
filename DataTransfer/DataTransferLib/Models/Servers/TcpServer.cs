using DataTransferLib.Models.Listeners;
using System.Net.Sockets;
using System.Text;
using DataTransferLib.Models.Clients;

namespace DataTransferLib.Models.Servers
{
    public class TcpServer : IServer
    {
        public EventHandler OnStarted;

        private TcpListener _server;
        private Thread _listenThread;

        public bool IsConnected { get; set; }

        public TcpServer(int port)
        {
            _server = new TcpListener(port);
            Start();
        }

        public void Start()
        {
            _server.Start();
            _listenThread = new Thread(Listen);
            IsConnected = true;
        }

        public void Listen()
        {
            while (IsConnected)
            {
                try
                {
                    IClient client = new Client(_server.AcceptTcpClient());
                    NetworkStream networkStream = client.GetStream();
                    string response = "Message received";
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    networkStream.Write(data, 0, data.Length);
                    networkStream.Close();
                }
                catch
                {
                    break;
                }

            }
            
        }

        public void Stop()
        {
            _server.Start();
            OnStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}
