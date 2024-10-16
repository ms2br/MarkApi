using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.RedisExceptions
{
    public class UnsupportedEndpointException : Exception,
        IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ExceptionMessage { get; }

        public UnsupportedEndpointException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
