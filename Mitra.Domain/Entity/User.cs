using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain.Entity
{
    public  class User: BaseEntity
    {
        public string Username { get; set; } 
        public string PasswordHash { get; set; } 
        public string ? Addresss { get; set; }
        public string  RefreshToken { get; set; } 
        public DateTime  TokenCreated { get; set; }
        public DateTime  TokenExpires { get; set; }

    }
}
