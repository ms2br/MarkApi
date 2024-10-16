using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.TokenDtos
{
    public class TokenParamDto
    {
        public AppUser AppUser { get; set; }
        public int Hours { get; set; }
    }
}
