using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public class Event:BaseEntity
    {
        public string EventName { get; set; }
        public string EventAddress { get; set; }
        public DateTime EventDate { get; set; }
        public int EventcategoryId { get; set; }

        public EventCategory Category { get; set; }
    }
}
