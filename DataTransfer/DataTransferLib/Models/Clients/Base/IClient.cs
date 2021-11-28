using System.Net.Sockets;

namespace DataTransferLib.Models.Clients.Base
{
    public interface IClient
    {
        void Start();
        void Listen();
        void Stop();
        NetworkStream GetStream();
    }
}