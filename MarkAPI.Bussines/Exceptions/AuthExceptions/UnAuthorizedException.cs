using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.AuthExceptions
{
    public class UnAuthorizedException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public string ExceptionMessage { get; }

        public UnAuthorizedException()
        {
            ExceptionMessage = "Unauthorized. Please log in.";
        }

        public UnAuthorizedException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
