using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.Bussines.Exceptions.AuthExceptions;
using MarkAPI.Bussines.Exceptions.IdentityExceptions;
using MarkAPI.Bussines.Helpers;
using MarkAPI.Bussines.Services.Interfaces;
using MarkAPI.CORE.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarkAPI.Bussines.Services.Implements
{
    public class UserService : IUserService
    {
        IMapper _mapper { get; }
        IEmailService _emailService { get; }
        IAuthService _authService { get; }
        IHttpContextAccessor _context { get; }
        IKeywordService _keywordService { get; }
        INoteService _noteService { get; }
        UserManager<AppUser> _um { get; }
        Lazy<string> userId => new Lazy<string>(_context.HttpContext.User.Identity.IsAuthenticated ? GetUserID() : throw new UnAuthorizedException());
        IBlackListService _blackList { get; }

        public UserService(IMapper mapper,
            UserManager<AppUser> um,
            IEmailService emailService,
            IAuthService authService,
            IHttpContextAccessor context,
            IBlackListService blackList,
            IKeywordService keywordService,
            INoteService noteService)
        {
            _mapper = mapper;
            _um = um;
            _emailService = emailService;
            _authService = authService;
            _context = context;
            _blackList = blackList;
            _keywordService = keywordService;
            _noteService = noteService;
        }

        public async Task CreateUserAsync(RegisterDto dto)
        {
            AppUser appUser = _mapper.Map<AppUser>(dto);
            IdentityResult success = await _um.CreateAsync(appUser, dto.Password);
            if (!success.Succeeded)
            {
                GetStringBuilder(success.Errors);
            }
            await _emailService.SendEmailConfirmedAsync(_mapper.Map<UserDto>(appUser));
        }


        public async Task EmailConfirmedAsync(string token, string userId)
        {
            AppUser appUser = await _um.FindByIdAsync(userId);

            if (appUser == null)
                throw new NotFoundException<AppUser>(ExceptionMessages.UserNotFoundMessage);

            token = HttpUtility.UrlDecode(token);

            if (!await _authService.VerifyEmailConfirmedTokenAsync(appUser, token))
                throw new EmailConfirmationFailedException();

            IdentityResult result = await _um.ConfirmEmailAsync(appUser, token);

            if (!result.Succeeded)
                throw new EmailConfirmationFailedException();

            result = await _um.UpdateSecurityStampAsync(appUser);

            if (!result.Succeeded)
                GetStringBuilder(result.Errors);

        }


        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            AppUser appUser = await _um.FindByIdAsync(userId.Value);
            UserChecking(appUser);
            IdentityResult result = await _um.ChangePasswordAsync(appUser, dto.CurrentPassword, dto.Passsword);
            if (!result.Succeeded)
                GetStringBuilder(result.Errors);
        }

        public async Task ChangeEmailAsync(ChangeEmailDto dto)
        {
            AppUser appUser = await _um.FindByIdAsync(userId.Value);
            UserChecking(appUser);
            string emailToken = await _um.GenerateChangeEmailTokenAsync(appUser, dto.Email);
            IdentityResult identityResult = await _um.ChangeEmailAsync(appUser, dto.Email, emailToken);
            if (!identityResult.Succeeded)
                GetStringBuilder(identityResult.Errors);
            appUser.UserName = "msbr";
            identityResult = await _um.UpdateSecurityStampAsync(appUser);
            if (!identityResult.Succeeded)
                GetStringBuilder(identityResult.Errors);
        }

        public async Task UserRemoveAsync()
        {
            await _keywordService.RemoveAllAsync();
            await _noteService.RemoveAllAsync();
            IdentityResult identityResult = await _um.DeleteAsync(await _um.FindByIdAsync(userId.Value));
            if (!identityResult.Succeeded)
                GetStringBuilder(identityResult.Errors);
            await _blackList.SetAsync(_context.HttpContext.GetUserToken());
        }

        void UserChecking(AppUser appUser)
        {
            if (appUser == null && !_um.IsEmailConfirmedAsync(appUser).Result)
                throw new NotFoundException<AppUser>(ExceptionMessages.UserNotFoundMessage);
        }

        void GetStringBuilder(IEnumerable<IdentityError> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in errors)
            {
                stringBuilder.AppendLine(error.Description);
            }
            throw new IdentityResultException(stringBuilder.ToString());
        }

        string GetUserID()
        {
            IEnumerable<Claim> claims = _context.HttpContext.User.Claims;
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
