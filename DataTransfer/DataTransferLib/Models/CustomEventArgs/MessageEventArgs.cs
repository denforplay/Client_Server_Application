using DataTransferLib.Models.Clients.Base;

namespace DataTransferLib.Models.CustomEventArgs
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
        public IClient Client { get; set; }
    }
}