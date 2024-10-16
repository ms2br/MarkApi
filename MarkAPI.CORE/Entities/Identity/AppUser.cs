using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.CORE.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string? ImgUrl { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
