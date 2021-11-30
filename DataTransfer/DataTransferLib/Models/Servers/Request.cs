using DataTransferLib.Core.Enums;

namespace DataTransferLib.Models.Servers
{
    public class Request
    {
        private HttpType _httpType;
        private string _url;
        private string _host;
        private string _data;

        public string Data => _data;

        public HttpType HttpType => _httpType;

        public Request(HttpType httpType, string url, string host, string data)
        {
            _httpType = httpType;
            _url = url;
            _host = host;
            _data = data;
        }

        public static Request GetRequest(string request)
        {
            string[] tokens = request.Split(' ');
            Enum.TryParse<HttpType>(tokens[0], out HttpType httpType);
            string data = tokens[1];
            string url = tokens[3].Split("\r\n")[0];
            string host = tokens[4];
            if (data == "gzip,")
                return new Request(httpType, url, host, "");
            return new Request(httpType, url, host, data);
        }
    }
}
