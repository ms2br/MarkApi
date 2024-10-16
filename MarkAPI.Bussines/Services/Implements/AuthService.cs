using MarkAPI.Bussines.Dtos.AuthDtos;
using MarkAPI.Bussines.Dtos.TokenDtos;
using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.Bussines.Exceptions.AuthExceptions;
using MarkAPI.Bussines.Exceptions.IdentityExceptions;
using MarkAPI.CORE.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarkAPI.Bussines.Services.Implements
{
    public class AuthService : IAuthService
    {
        UserManager<AppUser> _um { get; }
        ITokenService _tokenService { get; }
        IEmailService _emailService { get; }
        IMapper _mapper { get; }
        public AuthService(UserManager<AppUser> um,
            ITokenService tokenService,
            IMapper mapper,
            IEmailService emailService)
        {
            _um = um;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> VerifyEmailConfirmedTokenAsync(AppUser appUser, string token)
        {
            return await _um.VerifyUserTokenAsync(appUser,_um.Options.Tokens.EmailConfirmationTokenProvider, "EmailConfirmation",token);
        }
        
        public async Task<bool> VerifyForgotPasswordTokenAsync(AppUser appUser,string token)
        {
            return await _um.VerifyUserTokenAsync(appUser,_um.Options.Tokens.PasswordResetTokenProvider, "PasswordReset",token);
        }

        public async Task<TokenDto> LoginAsync(LoginDto dto)
        {

            AppUser? appUser = dto.NameOrEmail switch
            {
                _ when dto.NameOrEmail.Contains("@") == true => await _um.FindByEmailAsync(dto.NameOrEmail),
                _ => await _um.FindByNameAsync(dto.NameOrEmail)
            };

            UserChecking(appUser);
            if (!await _um.CheckPasswordAsync(appUser, dto.Password))
                throw new UserOrPassswordWrongException();
            
            return await _tokenService.CreateAccessTokenAsync(appUser.Id);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            AppUser appUser = await _um.FindByEmailAsync(dto.Email);
            UserChecking(appUser);
           string token = await _um.GeneratePasswordResetTokenAsync(appUser);
           token = HttpUtility.UrlEncode(token);
           UserDto user = _mapper.Map<UserDto>(appUser);
            _emailService.SendForgotPasswordAsync(user,token); 

        }

        public async Task ResetPasswordAsync(string userId, string token, ResetPasswordDto dto)
        {
            AppUser appUser = await _um.FindByIdAsync(userId);
            UserChecking(appUser);
            if (!await VerifyForgotPasswordTokenAsync(appUser, token))
                throw new ForgotPasswordFailedException();

           IdentityResult result =  await  _um.ResetPasswordAsync(appUser,token,dto.Password);

            if (result.Succeeded)
            {
                StringBuilder builder = new();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new IdentityResultException(builder.ToString());
            }
            await _um.UpdateSecurityStampAsync(appUser);
        }

        void UserChecking(AppUser appUser)
        {
           if(appUser is null && !_um.IsEmailConfirmedAsync(appUser).Result)
                throw new NotFoundException<AppUser>(ExceptionMessages.UserNotFoundMessage);
        }
    }
}
