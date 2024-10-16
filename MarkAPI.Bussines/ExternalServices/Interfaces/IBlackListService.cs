using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalServices.Interfaces
{
    public interface IBlackListService
    {
        Task SetAsync(string token);
        Task<bool> TokenCheckAsync(string token);
    }
}
