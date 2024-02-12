using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    [Serializable]
    public class UserProfile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public int LocationId { get; set; }
        public List<string> UserTypeList { get; set; }

    }
}
