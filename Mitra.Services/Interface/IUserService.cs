using Microsoft.AspNetCore.Http;
using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> RegisterUser(UserDTO userDto);
        Task<string> Login(UserDTO userDto);
        Task<string> RefreashToken(string refreshToken);
    }
}
