﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Dtos.EventCategory
{
    public class EventCategoryListDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }

    }
}
