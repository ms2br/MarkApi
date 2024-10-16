using MarkAPI.Bussines.Dtos.KeywordDtos;
using MarkAPI.Bussines.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Profiles
{
    public class KeywordMapping
        : Profile
    {
        public KeywordMapping()
        {
            CreateMap<KeywordCreateDto, Keyword>()
                .ForMember(x => x.NormalizedWord, opt => opt.MapFrom(dto => dto.Word.ToUpper()));

            CreateMap<KeywordUpdateDto, Keyword>()
                .ForMember(x => x.NormalizedWord, opt => opt.MapFrom(dto => dto.Word.ToUpper()));

            CreateMap<Keyword, KeywordItemDto>();
        }
    }
}
