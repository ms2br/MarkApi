using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.CORE.Entities.Common
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatTime { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
