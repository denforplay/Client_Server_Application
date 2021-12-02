using System.Net.Sockets;

namespace DataTransferLib.Models.Clients
{
    /// <summary>
    /// Represents http client
    /// </summary>
    public class HttpClient : Client
    {
        public HttpClient(TcpClient client) : base(client)
        {
        }

        /// <summary>
        /// HttpClient constructor
        /// </summary>
        /// <param name="ip">Client ip</param>
        /// <param name="port">Client port</param>
        public HttpClient(string ip, int port) : base(ip, port)
        {
        }
    }
}
