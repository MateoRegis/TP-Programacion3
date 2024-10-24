using System.Net;

namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public string? Msg { get; set; }
        public NotFoundException(HttpStatusCode code, string? msg)
        {
            Code = code;
            Msg = msg;
        }
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
