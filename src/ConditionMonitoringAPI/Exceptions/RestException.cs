using System;
using System.Net;

namespace ConditionMonitoringAPI.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public RestException(HttpStatusCode code, string message)
            :base(message)
        {
            StatusCode = code;
        }
    }
}
