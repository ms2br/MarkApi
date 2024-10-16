using MarkAPI.Bussines.Dtos.KeywordDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Interfaces
{
    public interface IKeywordService
        : IGenericService<Keyword, KeywordCreateDto>
    {
        Task UpdateAsync(string id,KeywordUpdateDto data);

        Task<bool> IsExistAsync(string? id);
    }
}
