using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.IdentityExceptions
{
    public class IdentityResultException : Exception,IBaseException
    {
        public int StatusCode => StatusCodes.Status422UnprocessableEntity;
        public string ExceptionMessage { get; }

        public IdentityResultException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
