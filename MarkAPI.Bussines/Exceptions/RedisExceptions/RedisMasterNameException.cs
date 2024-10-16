using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.RedisExceptions
{
    public class RedisMasterNameException : Exception,
        IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ExceptionMessage { get; }

        public RedisMasterNameException()
        {
            
        }

        public RedisMasterNameException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
