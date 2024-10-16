using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions
{
    public interface IBaseException
    {
        public int StatusCode { get;  }
        public string ExceptionMessage { get; }
    }
}
