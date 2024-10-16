using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.Bussines.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _service { get; }

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task CreateUserAsync(RegisterDto dto)
        => await _service.CreateUserAsync(dto);

        [HttpGet("[action]/{userId}/{token}")]
        public async Task<IActionResult> EmailConfirmedAsync(string userId, string token)
        {
            await _service.EmailConfirmedAsync(token, userId);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            await _service.ChangePasswordAsync(dto);
        }

        [HttpPatch("[action]")]
        public async Task ChangeEmailAsync(ChangeEmailDto dto)
        {
            await _service.ChangeEmailAsync(dto);
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task UserRemoveAsync()
        => await _service.UserRemoveAsync();
    }
}