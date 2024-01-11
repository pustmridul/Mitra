using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public  class Expectation:BaseEntity
    {
        public decimal Amount { get; set; }
        public int EventId { get; set; }
       
        public Event Event { get; set; }

        public int DonorId { get; set; }
      
        public Donor Donor { get; set; }

    }
}
