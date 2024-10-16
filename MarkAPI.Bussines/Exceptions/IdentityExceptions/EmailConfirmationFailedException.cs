using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.IdentityExceptions
{
    public class EmailConfirmationFailedException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ExceptionMessage { get; }

        public EmailConfirmationFailedException()
        {
            ExceptionMessage = "Email confirmation failed";
        }

        public EmailConfirmationFailedException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
