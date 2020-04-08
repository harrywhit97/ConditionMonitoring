using System;
using System.Net;

namespace ConditionMonitoringAPI.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public Exception Exception { get; set; }

        public RestException(HttpStatusCode code, Exception exception)
        {
            StatusCode = code;
            Exception = exception;
        }

        public RestException(HttpStatusCode code, string exceptionMessage)
        {
            StatusCode = code;
            Exception = new Exception(exceptionMessage);
        }

        public RestException(HttpStatusCode code)
        {
            StatusCode = code;
            Exception = new Exception();
        }
    }
}
