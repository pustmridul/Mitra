using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Dtos
{
    public class ExpectationDto
    {
        public decimal Amount { get; set; }
        public int EventId { get; set; }
        public int DonorId { get; set; }
    }
}
