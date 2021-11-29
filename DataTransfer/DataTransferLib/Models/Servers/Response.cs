using DataTransferLib.Models.Servers.Base;
using System.Net.Sockets;
using System.Text;

namespace DataTransferLib.Models.Servers
{
    public class Response
    {
        private byte[] _data;
        private string _contentType;
        private string _status;

        public Response(string status, string contentType, byte[] data)
        {
            _data = data;
            _contentType = contentType;
            _status = status;
        }

        public static Response From(Request request)
        {
            if (request is null)
                return BadRequest();

            if (request.HttpType == Core.Enums.HttpType.GET)
            {
                return new Response("200 OK", "text/html", Encoding.ASCII.GetBytes(request.Data));
            }
            else
            {
                return NotAllowedRequest();
            }
        }

        private static Response BadRequest()
        {
            return new Response("400 Bad request", "html/text", new byte[0]);
        }

        private static Response NotAllowedRequest()
        {
            return new Response("405 - Method not allowed", "html/text", new byte[0]);
        }

        public void Post(NetworkStream networkStream)
        {
            string r = $"{HttpServer.VERSION} {_status}\n" +
                $"Content-type: {_contentType}\n" +
                $"Content-length: {_data.Length}";
            r += $"\n\nReceived data: {Encoding.UTF8.GetString(_data)}";
            byte[] data = Encoding.ASCII.GetBytes(r);
            networkStream.Write(data, 0, data.Length);
        }
    }
}