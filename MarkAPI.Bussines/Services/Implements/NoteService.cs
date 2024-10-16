using MarkAPI.Bussines.Dtos.NoteDtos;
using MarkAPI.Bussines.Exceptions.AuthExceptions;
using MarkAPI.CORE.Entities.Common;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Implements
{
    public class NoteService : INoteService
    {

        IKeywordService _keywordService { get; }
        IMapper _mapper { get; }
        INoteRepository _repo { get; }
        IHttpContextAccessor _http { get; }
        Lazy<string> userId => new Lazy<string>(_http.HttpContext.User.Identity.IsAuthenticated ? GetUserID() : throw new UnAuthorizedException());
        public NoteService(IMapper mapper, INoteRepository repo, IHttpContextAccessor http, IKeywordService keywordService)
        {
            _mapper = mapper;
            _repo = repo;
            _http = http;
            _keywordService = keywordService;
        }

        public async Task CreateAsync(NoteCreateDto data)
        {
            Note item = _mapper.Map<Note>(data);
            await CheckNoteAsync(item.Content.ToUpper());
            await CheckKeywordAsync(data.KeywordIds);
            foreach (var keywordId in data.KeywordIds)
                item.Keywords.Add(new NoteKeyword
                {
                    KeywordId = keywordId,
                });
            item.UserId = userId.Value;
            await _repo.CreateAsync(item);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(string? id, NoteUpdateDto data, params string[] includes)
        {
            var note = await CheckIdAsync(id, false, includes);
            if (!await CheckNoteAsync(data.Content.ToUpper(), data.KeywordIds, note)) 
                return;
            note = await updateKeywordAsync(data, note);
            _mapper.Map(data, note);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(params string[] includes) where T : class
        {
            return _mapper.Map<IEnumerable<T>>(await _repo.GetAllAsync(userId: userId.Value, includes: includes));
        }

        public async Task<T> GetByIdAsync<T>(string? id, params string[] includes) where T : class
        {
            return _mapper.Map<T>(await CheckIdAsync(id, true, includes));
        }

        public async Task RemoveAsync(string? id)
        {
            var note = await CheckIdAsync(id, false);
            await _repo.RemoveAsync(note);
            await _repo.SaveChangesAsync();
        }

        public async Task SoftRemoveAsync(string? id)
        {
            Note note = await CheckIdAsync(id, false);
            note.IsDeleted = true;
            await _repo.SaveChangesAsync();
        }

        public async Task<Note> CheckIdAsync(string? id, bool noTracking = true, params string[] includes)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();
            Note note = await _repo.GetByIdAsync(x => x.UserId == userId.Value && id == x.Id, noTracking, includes);
            if (note is null)
                throw new NotFoundException<Note>();
            return note;
        }


        public async Task RemoveAllAsync(params string[] includes)
        => await _repo.RemoveAllAsync(userId.Value,includes);

        async Task<Note> updateKeywordAsync(NoteUpdateDto dto, Note data)
        {
            if (!Enumerable.SequenceEqual(dto.KeywordIds, data.Keywords.Select(x => x.KeywordId)))
            {
                await CheckKeywordAsync(dto.KeywordIds);
                data.Keywords.Clear();
                foreach (var id in dto.KeywordIds)
                    data.Keywords.Add(new NoteKeyword
                    {
                        KeywordId = id
                    });
            }
            return data;
        }

        async Task CheckKeywordAsync(IEnumerable<string> keywordIds)
        {
            foreach (string id in keywordIds)
                if (!(string.IsNullOrWhiteSpace(id) || await _keywordService.IsExistAsync(id)))
                    throw new NotFoundException<Keyword>();
        }

        async Task CheckNoteAsync(string normalizedConent)
        {
            if (await _repo.IsExistAsync(x => x.UserId == userId.Value && x.Content.ToUpper() == normalizedConent))
                throw new IsExistException<Note>();
        }

        async Task<bool> CheckNoteAsync(string normalizedConent, IEnumerable<string> keywordIds, Note data)
        {
            if (await _repo.IsExistAsync(x => x.Content.ToUpper() == normalizedConent))
                return false;
            if (Enumerable.SequenceEqual(keywordIds, data.Keywords.Select(x => x.Note).Select(x => x.Id)))
                return false;
            return true;
        }

        string GetUserID()
        {
            IEnumerable<Claim> claims = _http.HttpContext.User.Claims;
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
