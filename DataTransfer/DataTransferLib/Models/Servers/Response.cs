using DataTransferLib.Core.Enums;
using DataTransferLib.Models.Servers.Base;
using MatrixLib.Models;
using MatrixLib.Models.Parsers;
using MatrixLib.Models.Parsers.MatrixParsers;
using MatrixLib.Models.SleSolutionMethods;
using System.Net.Sockets;
using System.Text;

namespace DataTransferLib.Models.Servers
{
    /// <summary>
    /// Represents server response
    /// </summary>
    public class Response
    {
        private byte[] _data;
        private string _contentType;
        private string _status;

        /// <summary>
        /// Response constructor
        /// </summary>
        /// <param name="status">Server status</param>
        /// <param name="contentType">Type of response content</param>
        /// <param name="data">Response data</param>
        public Response(string status, string contentType, byte[] data)
        {
            _data = data;
            _contentType = contentType;
            _status = status;
        }

        /// <summary>
        /// Method to create response from request
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Server response</returns>
        public static Response From(Request request)
        {
            if (request is null)
                return BadRequest();

            string[] splittedData = request.Data.Split('?');
            if (splittedData.Length > 1 && Enum.TryParse(splittedData[1], out RequestTypes requestType))
            {
                if (requestType == RequestTypes.SolveSle)
                {
                    IParser<SLE> sleFromBrowser = new SleFromUrl();
                    var sle = sleFromBrowser.ParseFrom(splittedData[0]);
                    ISleSolutionMethod solutionMethod = new GaussSolutionMethod(sle);
                    var solutions = solutionMethod.SolveSle();
                    return new Response("200 OK", "text/html", Encoding.UTF8.GetBytes("Solutions: " + solutions.ToString()));
                }
            }

            if (request.HttpType == HttpType.GET)
            {
                string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + HttpServer.WEB_FOLDER + request.Data[1..];
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

        /// <summary>
        /// Method to create response on bad request
        /// </summary>
        /// <returns>Returns 400 status error response</returns>
        private static Response BadRequest()
        {
            return new Response("400 Bad request", "html/text", new byte[0]);
        }

        /// <summary>
        /// Method to create response on not allowed request
        /// </summary>
        /// <returns>Returns response on not allowed request</returns>
        private static Response NotAllowedRequest()
        {
            return new Response("405 - Method not allowed", "html/text", new byte[0]);
        }

        /// <summary>
        /// Send response method
        /// </summary>
        /// <param name="networkStream">Client stream where to send response</param>
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