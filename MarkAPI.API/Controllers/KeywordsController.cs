using MarkAPI.Bussines.Dtos.KeywordDtos;
using MarkAPI.Bussines.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        IKeywordService _service { get; }

        public KeywordsController(IKeywordService service)
        {
            _service = service;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        => Ok(await _service.GetAllAsync<KeywordItemDto>("AppUser"));

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
            => Ok(await _service.GetByIdAsync<KeywordItemDto>(id, "AppUser")); 
        
        
        [HttpPost("[action]")]
        public async Task CreateAsync(KeywordCreateDto data)
        => await _service.CreateAsync(data);


        [HttpPut("[action]/{id?}")]
        public async Task UpdateAsync(string? id, KeywordUpdateDto data)
        => await _service.UpdateAsync(id, data);

        [HttpDelete("[action]/{id?}")]
        public async Task SoftRemoveAsync(string? id)
        => await _service.SoftRemoveAsync(id);

        [HttpDelete("[action]/{id?}")]
        public async Task RemoveAsync(string? id)
        => await _service.RemoveAsync(id);
    }
}
