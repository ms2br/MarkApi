using MarkAPI.CORE.Entities.Common;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.CORE.Entities
{
    public class Keyword : BaseEntity
    {
        public string Word { get; set; }
        public string NormalizedWord { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<NoteKeyword> Notes{ get; set; }
    }
}
