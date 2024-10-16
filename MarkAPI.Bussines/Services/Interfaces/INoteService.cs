using MarkAPI.Bussines.Dtos.NoteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Interfaces
{
    public interface INoteService
        : IGenericService<Note, NoteCreateDto>
    {
        Task UpdateAsync(string? id, NoteUpdateDto data, params string[] includes);
    }
}
