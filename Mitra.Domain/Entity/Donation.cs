﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public  class Donation:BaseEntity
    {
        public decimal Amount { get; set; }
        public int EventId { get; set; }
        public int DonorId { get; set; }
        public DateTime? EventDate { get; set; }
        public Event Event { get; set; }
        public Donor Donor { get; set; }
    }
}
