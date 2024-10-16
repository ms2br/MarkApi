using MarkAPI.Bussines.Dtos.KeywordDtos;
using MarkAPI.Bussines.Dtos.NoteDtos;
using MarkAPI.Bussines.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Profiles
{
    public class NoteMapping : Profile
    {
        public NoteMapping()
        {
            CreateMap<NoteCreateDto, Note>();

            CreateMap<NoteUpdateDto, Note>();

            CreateMap<Note, NoteItemDto>()
                .AfterMap((src, dest, context) =>
                {
                    dest.KeywordDtos = context.Mapper.Map<IEnumerable<KeywordItemDto>>(src.Keywords.Select(x => x.Keyword));
                    dest.AppUser = context.Mapper.Map<UserDto>(src.AppUser);
                });
        }
    }
}
