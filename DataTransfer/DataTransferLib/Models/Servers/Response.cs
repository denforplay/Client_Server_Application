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
                string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\MatrixLib\\Views\\" + request.Data[1..];
                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    FileStream fs = fileInfo.OpenRead();
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] data = new byte[fs.Length];
                    br.Read(data, 0, data.Length);
                    br.Close();
                    return new Response("200 OK", "text/html", data);

                }

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
            string msg = "HTTP/1.1 200 OK\n" +
                     $"Content-Type: {_contentType}\n" +
                     $"Content-Length: {_data.Length}" + "\r\n\r\n"
                     + Encoding.UTF8.GetString(_data);

            byte[] responseMsg = Encoding.UTF8.GetBytes(msg);

            networkStream.Write(responseMsg, 0, responseMsg.Length);
        }
    }
}