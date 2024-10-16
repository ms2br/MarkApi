 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.AuthExceptions
{
    public class UserOrPassswordWrongException : Exception, IBaseException 
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public string ExceptionMessage { get; }

        public UserOrPassswordWrongException(string? message)
        {
            ExceptionMessage = message;
        }

        public UserOrPassswordWrongException()
        {
            ExceptionMessage = ExceptionMessages.UserOrPassswordWrongMessage;
        }
    }
}
