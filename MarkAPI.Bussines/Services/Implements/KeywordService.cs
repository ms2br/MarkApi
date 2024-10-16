using MarkAPI.Bussines.Dtos.KeywordDtos;
using MarkAPI.Bussines.Exceptions.AuthExceptions;
using MarkAPI.CORE.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Implements
{
    public class KeywordService : IKeywordService
    {
        IKeywordRepository _keywordRepo { get; }
        IMapper _mapper { get; }
        IHttpContextAccessor _httpContext { get; }
        Lazy<string> userId => new Lazy<string>(_httpContext.HttpContext.User.Identity.IsAuthenticated ? GetUserID() : throw new UnAuthorizedException());

        public KeywordService(IKeywordRepository keywordRepo, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _keywordRepo = keywordRepo;
            _mapper = mapper;
            _httpContext = httpContext;
        }


        public async Task CreateAsync(KeywordCreateDto data)
        {
            var item = _mapper.Map<Keyword>(data);
            await CheckKeywordAsync(item.NormalizedWord);
            item.UserId = userId.Value;
            await _keywordRepo.CreateAsync(item);
            await _keywordRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(params string[] includes) where T : class
        => _mapper.Map<IEnumerable<T>>(await _keywordRepo.GetAllAsync(includes: includes, userId: userId.Value));

        public async Task<T> GetByIdAsync<T>(string id, params string[] includes) where T : class
        => _mapper.Map<T>(await CheckIdAsync(id, true, includes));

        public async Task UpdateAsync(string? id, KeywordUpdateDto data)
        {
            await CheckKeywordAsync(data.Word.ToUpper());
            var item = await CheckIdAsync(id, false);
            _mapper.Map(data, item);
            await _keywordRepo.SaveChangesAsync();
        }

        public async Task<Keyword> CheckIdAsync(string? id, bool noTracking = true, params string[] includes)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException();
            Keyword data = await _keywordRepo.GetByIdAsync(x => x.Id == id && userId.Value == x.UserId, noTracking, includes);
            if (data is null)
                throw new NotFoundException<Keyword>();
            return data;
        }

        public async Task RemoveAsync(string? id)
        {
            await _keywordRepo.RemoveAsync(await CheckIdAsync(id, false));
            await _keywordRepo.SaveChangesAsync();
        }

        public async Task SoftRemoveAsync(string? id)
        {
            var item = await CheckIdAsync(id, false);
            item.IsDeleted = true;
            await _keywordRepo.SaveChangesAsync();
        }

        public async Task<bool> IsExistAsync(string? id)
        => await _keywordRepo.IsExistAsync(x => x.Id == id && userId.Value == x.UserId);

        public async Task RemoveAllAsync(params string[] includes)
        => await _keywordRepo.RemoveAllAsync(userId.Value, includes);

        async Task CheckKeywordAsync(string normalizedWord)
        {
            if (await _keywordRepo.IsExistAsync(x => x.NormalizedWord == normalizedWord && x.UserId == userId.Value))
                throw new IsExistException<Keyword>();
        }

        string GetUserID()
        {
            return _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

    }
}
