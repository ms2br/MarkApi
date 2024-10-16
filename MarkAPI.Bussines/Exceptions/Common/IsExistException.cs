using MarkAPI.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.Common
{
    public class IsExistException<T>
        : Exception, IBaseException
        where T : BaseEntity
    {
        public int StatusCode => StatusCodes.Status409Conflict;

        public string ExceptionMessage { get; }

        public IsExistException()
        {
            ExceptionMessage = $"{typeof(T).Name} Already Add";
        }

        public IsExistException(string? message)
        {
            ExceptionMessage = message;
        }
    }
}
