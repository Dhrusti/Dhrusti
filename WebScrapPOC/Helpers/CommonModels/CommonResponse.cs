using System.Net;

namespace WebScrapPOC.Helpers.CommonModels
{
    public class CommonResponse
    {
        public bool Status { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadGateway;
        public string Message { get; set; } = "Something wwnt wrong! Please try again!";
        public dynamic Data { get; set; } = null;
    }
}
