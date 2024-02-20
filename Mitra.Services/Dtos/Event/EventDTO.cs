using Mitra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Dtos.Event
{
    public class EventDTO : BaseEntity
    {
        public string EventName { get; set; }
        public string EventAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventcategoryId { get; set; }
    }
}
