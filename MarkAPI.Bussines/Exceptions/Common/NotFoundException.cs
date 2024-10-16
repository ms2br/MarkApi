using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.Common
{
    public class NotFoundException<T> : Exception, IBaseException
     where T : class
    {
        public int StatusCode => StatusCodes.Status404NotFound;

        public string ExceptionMessage { get; }

        public NotFoundException()
        {
            ExceptionMessage = $"{typeof(T).Name} Not Found";
        }

        public NotFoundException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
