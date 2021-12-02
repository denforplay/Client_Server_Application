using System.Net.Sockets;

namespace DataTransferLib.Models.Clients.Base
{
    /// <summary>
    /// Describe base client functionality
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Start listen server answer
        /// </summary>
        void Start();

        /// <summary>
        /// Listen answer from server
        /// </summary>
        void Listen();
        
        /// <summary>
        /// Stop listen answer from server
        /// </summary>
        void Stop();

        /// <summary>
        /// Send message on server
        /// </summary>
        /// <param name="content">Message content</param>
        void SendMessage(string content);

        /// <summary>
        /// Method to get current client stream
        /// </summary>
        /// <returns>Client stream</returns>
        NetworkStream GetStream();
    }
}