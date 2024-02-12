using Microsoft.AspNetCore.Http;
using Mitra.Domain.Entity;
using Mitra.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public int UserId { get; }
        public string Username { get; }
        public int LocationId { get; }
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"));
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue("user_name");
            LocationId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("locationId"));

        }

        public UserProfile Current()
        {
            var currentUser = _contextAccessor.HttpContext?.User;


            if (currentUser == null)
                return new UserProfile();

            if (!currentUser.HasClaim(c => c.Type == ClaimTypes.UserData))
                return new UserProfile();

            var userData = currentUser.Claims.Single(c => c.Type == ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<UserProfile>(userData);

        }
    }
}
