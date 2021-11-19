using System.Net.Sockets;

namespace DataTransferLib.Models.Clients
{
    public interface IClient
    {
        void Start();
        void Listen();
        void Stop();
        NetworkStream GetStream();
    }
}