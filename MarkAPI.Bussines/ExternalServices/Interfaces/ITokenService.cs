using MarkAPI.Bussines.Dtos.TokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalServices.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDto> CreateAccessTokenAsync(string userId);
    }
}
