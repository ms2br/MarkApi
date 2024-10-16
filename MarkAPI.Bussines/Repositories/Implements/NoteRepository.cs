using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Repositories.Implements
{
    public class NoteRepository : GenericRepository<Note>,
        INoteRepository
    {
        public NoteRepository(MarkDbContext db) : base(db)
        {
        }
    }
}
