using Mitra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        public string Username { get; }

        public UserProfile Current();

    }
}
