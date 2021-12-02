using DataTransferLib.Models.Listeners;
using System.Net.Sockets;
using System.Text;
using DataTransferLib.Models.Clients;
using System.Net;
using DataTransferLib.Models.Clients.Base;

namespace DataTransferLib.Models.Servers
{
    /// <summary>
    /// Represents tcp server
    /// </summary>
    public class TcpServer : IServer
    {
        public delegate void ReceiveData(IClient client, string message);
        public event ReceiveData DataReceiveEvent;

        protected TcpListener _server;
        private Thread _listenThread;

        /// <summary>
        /// Property shown if server is started
        /// </summary>
        public bool IsStarted { get; set; }

        /// <summary>
        /// Tcp server constructor
        /// </summary>
        /// <param name="ip">Server ip</param>
        /// <param name="port">Servevr port</param>
        /// <exception cref="ArgumentNullException">Thrown if ip string is null or empty</exception>
        public TcpServer(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip))
            {
                throw new ArgumentNullException();
            }

            if (IPAddress.TryParse(ip, out var serverIp))
            {
                _server = new TcpListener(serverIp, port);
            }

            DataReceiveEvent += (client, msg) => SendAnswer(client, msg + " RECEIVED SUCCESFULLY");

            Start();
        }

        /// <summary>
        /// Method that start server(listen client connections)
        /// </summary>
        public virtual void Start()
        {
            _listenThread = new Thread(Listen);
            _listenThread.Start();
        }

        /// <summary>
        /// Method that listens client connections
        /// </summary>
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

        /// <summary>
        /// Method for receiving message from client
        /// </summary>
        /// <param name="client"></param>
        protected virtual void ReceiveMessage(IClient client)
        {
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[256];
            client.GetStream().Read(data);
            builder.AppendLine(Encoding.UTF8.GetString(data));
            OnDataReceivedEvent(client, builder.ToString());
            client.Stop();
        }

        /// <summary>
        /// Method for sending answer to client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        protected virtual void SendAnswer(IClient client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.GetStream().Write(data, 0, data.Length);
        }

        /// <summary>
        /// Method for calling data received event
        /// </summary>
        /// <param name="client">Client to send answer</param>
        /// <param name="message">Message to send</param>
        protected void OnDataReceivedEvent(IClient client, string message)
        {
            DataReceiveEvent?.Invoke(client, message);
        }

        /// <summary>
        /// Method that stop server(stop listening incoming connections)
        /// </summary>
        public void Stop()
        {
            _server.Stop();
        }
    }
}
