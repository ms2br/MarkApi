using MarkAPI.Bussines.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(RegisterDto dto);
        Task EmailConfirmedAsync(string token, string userId);
        Task ChangePasswordAsync(ChangePasswordDto dto);
        Task ChangeEmailAsync(ChangeEmailDto dto);
        Task UserRemoveAsync();
    }
}
