using DataTransferLib.Core.Enums;

namespace DataTransferLib.Models.Servers
{
    /// <summary>
    /// Represents request on server
    /// </summary>
    public class Request
    {
        private HttpType _httpType;
        private string _url;
        private string _host;
        private string _data;

        /// <summary>
        /// Url
        /// </summary>
        public string Url => _url;

        /// <summary>
        /// Sended data
        /// </summary>
        public string Data => _data;

        /// <summary>
        /// Type of request
        /// </summary>
        public HttpType HttpType => _httpType;

        /// <summary>
        /// Request constructor
        /// </summary>
        /// <param name="httpType">Request type</param>
        /// <param name="url">Url</param>
        /// <param name="host">Host</param>
        /// <param name="data">Sended data</param>
        public Request(HttpType httpType, string url, string host, string data)
        {
            _httpType = httpType;
            _url = url;
            _host = host;
            _data = data;
        }

        /// <summary>
        /// Method to create request
        /// </summary>
        /// <param name="request">Request string</param>
        /// <returns>New request</returns>
        public static Request GetRequest(string request)
        {
            string[] tokens = request.Split(' ');
            Enum.TryParse(tokens[0], out HttpType httpType);
            string data = tokens[1];
            string url = tokens[3].Split("\r\n")[0];
            string[] parsedData = data.Split('?');
            string host = tokens[4];
            if (parsedData.Length > 1)
            {
                return new Request(httpType, url, host, parsedData[1] + $"?{RequestTypes.SolveSle}");
            }
            return new Request(httpType, url, host, data);
        }
    }
}
