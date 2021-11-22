using DataTransferLib.Models.Listeners;
using System.Net.Sockets;
using System.Text;
using DataTransferLib.Models.Clients;

namespace DataTransferLib.Models.Servers
{
    public class TcpServer : IServer
    {
        public event Action OnStarted;
        public event Action OnDataReceived;
        public event Action OnAnswerSended;

        private TcpListener _server;
        private Thread _listenThread;

        public bool IsStarted { get; set; }

        public TcpServer(int port)
        {
            _server = new TcpListener(port);
            Start();
        }

        public void Start()
        {
            _server.Start();
            _listenThread = new Thread(Listen);
            IsStarted = true;
            OnStarted?.Invoke();
        }

        public void Listen()
        {
            while (IsStarted)
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
            OnStarted?.Invoke();
        }
    }
}
