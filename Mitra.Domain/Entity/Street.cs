using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public class Street : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Donor>? Donors { get; set; }
    }
}
