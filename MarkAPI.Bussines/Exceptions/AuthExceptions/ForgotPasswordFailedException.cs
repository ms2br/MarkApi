using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Exceptions.AuthExceptions
{
    public class ForgotPasswordFailedException :
        Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ExceptionMessage { get; }

        public ForgotPasswordFailedException()
        {
            ExceptionMessage = "Password reset failed.";
        }
    }
}
