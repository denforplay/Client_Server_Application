using System.Net;

namespace DataTransferLib.Models.Listeners
{
    public interface IServer
    {
        bool IsConnected { get; set; }
        void Start();
        void Listen();
        void Stop();
    }
}
