using MarkAPI.Bussines.Dtos.AuthDtos;
using MarkAPI.Bussines.Dtos.TokenDtos;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto dto);

        Task ForgotPasswordAsync(ForgotPasswordDto dto);

        Task ResetPasswordAsync(string userId,string token,ResetPasswordDto dto);

        Task<bool> VerifyEmailConfirmedTokenAsync(AppUser appUser,string token);
    }
}
