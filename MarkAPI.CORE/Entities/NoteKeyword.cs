using MarkAPI.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.CORE.Entities
{
    public class NoteKeyword : BaseEntity
    {
        public string KeywordId { get; set; }
        public string NoteId { get; set; }
        public Keyword Keyword { get; set; }
        public Note Note { get; set; }
    }
}
