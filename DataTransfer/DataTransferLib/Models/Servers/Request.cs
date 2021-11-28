using DataTransferLib.Core.Enums;

namespace DataTransferLib.Models.Servers
{
    public class Request
    {
        private HttpType _httpType;
        private string _url;
        private string _host;

        public HttpType HttpType => _httpType;

        public Request(HttpType httpType, string url, string host)
        {
            _httpType = httpType;
            _url = url;
            _host = host;
        }

        public static Request GetRequest(string request)
        {
            string[] tokens = request.Split(' ');
            Enum.TryParse<HttpType>(tokens[0], out HttpType httpType);
            string url = tokens[1];
            string host = tokens[4];
            return new Request(httpType, url, host);
        }
    }
}
