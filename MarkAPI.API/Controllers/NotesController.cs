using MarkAPI.Bussines.Dtos.NoteDtos;
using MarkAPI.Bussines.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INoteService _service { get; }
        public NotesController(INoteService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task CreateAsync(NoteCreateDto dto)
        {
             await _service.CreateAsync(dto);
        }

        [HttpPost("[action]/{id?}")]
        public async Task UpdateAsync(string? id, NoteUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync<NoteItemDto>("Keywords.Keyword","AppUser"));
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            return Ok(_service.GetByIdAsync<NoteItemDto>(id, "Keywords.Keyword", "AppUser"));
        }

        [HttpDelete("[action]/{id?}")]
        public async Task RemoveAsync(string? id)
        {
            await _service.RemoveAsync(id);
        }

        [HttpDelete("[action]/{id?}")]
        public async Task SoftRemoveAsync(string? id)
        {
            await _service.SoftRemoveAsync(id);
        }
    }
}
