using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.KeywordDtos
{
    public class KeywordItemDto
    {
        public string Id { get; set; }
        public string Word { get; set; }
        public string NormalizedWord { get; set; }
        public DateTime CreatTime { get; set; }
        public UserDto AppUser { get; set; }
        // TODO: Note Daxil Et
    }
}
