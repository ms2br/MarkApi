using MarkAPI.Bussines.Dtos.KeywordDtos;
using MarkAPI.Bussines.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.NoteDtos
{
    public class NoteItemDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatTime { get; set; }
        public IEnumerable<KeywordItemDto> KeywordDtos { get; set; }
        public UserDto AppUser { get; set; }
    }
}
