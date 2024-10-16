using MarkAPI.CORE.Entities.Common;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.CORE.Entities
{
    public class Note : BaseEntity
    {
        public string Content { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<NoteKeyword> Keywords { get; set; }

        public Note()
        {
            Keywords = new HashSet<NoteKeyword>();
        }
    }
}
