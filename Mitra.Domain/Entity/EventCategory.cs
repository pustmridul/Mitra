using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public class EventCategory:BaseEntity
    {
        public string CategoryName { get; set; }
        public string ParentId { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
