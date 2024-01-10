using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public class Donor: BaseEntity
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int userId {  get; set; }
        public User User { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Expectation> Expectations { get; set; }
    }
}
