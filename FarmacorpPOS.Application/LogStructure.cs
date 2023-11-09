using System.Net;

namespace FarmacorpPOS.Application
{
    public class LogStructure
    {
        public HttpMethod HttpMethod { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Result { get; set; }
        public string Message { get; set; }
    }
}