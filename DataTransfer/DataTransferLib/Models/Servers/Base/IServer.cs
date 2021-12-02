using System.Net;

namespace DataTransferLib.Models.Listeners
{
    /// <summary>
    /// Describes base server functionality
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Property showed is server started or not
        /// </summary>
        bool IsStarted { get; set; }

        /// <summary>
        /// Start listen clients
        /// </summary>
        void Start();

        /// <summary>
        /// Listen clients
        /// </summary>
        void Listen();

        /// <summary>
        /// Stop listen clients
        /// </summary>
        void Stop();
    }
}
