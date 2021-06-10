using System.Net;

namespace AuctimoTraders.Helpers
{
    /// <summary>
    ///     Default API response model for a request made to the api
    /// </summary>
    public class APIResponseMessage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="statusCode"></param>
        public APIResponseMessage(string message, string[] errors, HttpStatusCode statusCode, object result=null)
        {
            Message = message;
            Errors = errors;
            StatusCode = statusCode;
            Result = result;
        }

        /// <summary>
        ///     Response message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Errors that occurred when executing the response
        /// </summary>
        public string[] Errors { get; set; }

        /// <summary>
        ///     <see cref="HttpStatusCode"/>
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///     Result returned for the query
        /// </summary>
        public object Result { get; set; }
    }
}