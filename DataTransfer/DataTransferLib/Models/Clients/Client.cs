using DataTransferLib.Models.Clients.Base;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataTransferLib.Models.Clients
{
    /// <summary>
    /// Represents tcp client
    /// </summary>
    public class Client : IClient
    {
        protected TcpClient _client;
        private Thread _listenThread;
        private NetworkStream _networkStream => _client.GetStream();
        
        /// <summary>
        /// Property to chech if client is connected to server
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Client constructor
        /// </summary>
        /// <param name="client">Tcp client connected to server</param>
        public Client(TcpClient client)
        {
            _client = client;
            Start();
        }

        /// <summary>
        /// Client constructor
        /// </summary>
        /// <param name="ip">Client ip</param>
        /// <param name="port">Client port</param>
        /// <exception cref="Exception">Throws if ip is not ip</exception>
        public Client(string ip, int port)
        {
            _client = new TcpClient();

            if (IPAddress.TryParse(ip, out IPAddress clientIP))
            {
                _client.Connect(clientIP, port);
            }
            else
            {
                throw new Exception();
            }

            Start();
        }

        public void Start()
        {
            _listenThread = new Thread(Listen);
            _listenThread.Start();
            IsConnected = true;
        }

        public void Stop()
        {
            _client.Close();
            IsConnected = false;
        }

        public void Listen()
        {
            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            while (_networkStream.DataAvailable && IsConnected)
            {
                int bytes = _networkStream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
        }

        public virtual void SendMessage(string content)
        {
            string message = content;
            byte[] data = Encoding.UTF8.GetBytes(message);
            _networkStream.Write(data, 0, data.Length);
        }

        public NetworkStream GetStream() => _networkStream;

        public override bool Equals(object? obj)
        {
            if (obj is Client)
            {
                return ReferenceEquals(obj, this);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _client.GetHashCode() + _listenThread.GetHashCode();
        }
    }
}