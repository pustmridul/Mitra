using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azure;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace Mitra.Services.Services
{
    public class UserService :IUserService
    {
        private IMapper _mapper;
        private AppDbContext _db;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper,AppDbContext appDbContext,IConfiguration configuration)
        {
            _mapper = mapper;
            _db = appDbContext;
            _configuration = configuration;
        }
        public HttpContext context;
        public async Task<string> Login(UserDTO userDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if ( user.Username != userDto.Username)
            {
                return "User does not exist";
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.PasswordHash, user.PasswordHash))
            {
                return "worng Password";
            }

            _mapper.Map<UserDTO>(user);
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(user,refreshToken);
            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var crods = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: crods
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        

        public async Task<string> RefreashToken(string refreshToken)
        {


            var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.TokenExpires < DateTime.Now)
            {
                return "Invalid Refresh Token or Token expired.";
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(user, newRefreshToken);

            return token;
        }


        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7)
            };
            return refreshToken;
        }

        private void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            context.Response.Cookies.Append("refreshToken", newRefreshToken.Token);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _db.SaveChanges();
        }

        public async Task<List<UserDTO>> RegisterUser(UserDTO userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            var users = _mapper.Map<User>(userDto);

            users.PasswordHash = passwordHash;
            _db.Users.Add(users);
            await _db.SaveChangesAsync();

            var upuser = await _db.Users
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return upuser;
        }
    }
}
