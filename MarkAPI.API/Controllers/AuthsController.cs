using MarkAPI.Bussines.Dtos.AuthDtos;
using MarkAPI.Bussines.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        public IAuthService _authService { get; }
        
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {            
            return Ok(await _authService.LoginAsync(dto));
        }

        [HttpPost("[action]")]    
        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            await _authService.ForgotPasswordAsync(dto);
        }

        [HttpPost("[action]/{userId}/{token}")]        
        public async Task ResetPasswordAsync(string userId,string token,ResetPasswordDto passwordDto)
        {
            await _authService.ResetPasswordAsync(userId, token, passwordDto);
        }
    }
}
