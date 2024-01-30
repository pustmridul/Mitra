using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Dtos
{
    public class EventListDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventCategoryName { get; set; }
       // public int EventcategoryId { get; set; }
    }
}
