using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Helpers
{
    public static class GetToken
    {
        public static string GetUserToken(this HttpContext context)
        => context.Request.Headers[HeaderNames.Authorization].ToString();
    }
}