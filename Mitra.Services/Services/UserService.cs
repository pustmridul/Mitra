using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;



namespace Mitra.Services.Services
{
    public class UserService :IUserService
    {
        private IMapper _mapper;
        private AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IMapper mapper,AppDbContext appDbContext,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _db = appDbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<LoginResponse> Login(LoginDto userDto)
        {
             var result = new LoginResponse();

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if ( user.Username != userDto.Username)
            {
                result.Message = "User Not Exit";   
                return result;
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.PasswordHash, user.PasswordHash))
            {
                result.Message = "InValid Password";
                return result;
            }

           var nUser =  _mapper.Map<UserDTO>(user);
            string token = CreateToken(nUser);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(nUser, refreshToken);
            
            result.Token = token;
            result.Username= userDto.Username;
            result.Id = user.Id;

            return result;
        }

        private string CreateToken( UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
               
                
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim( "userName", user.Username),

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
            var nUser = _mapper.Map<UserDTO>(user);
            string token = CreateToken(nUser);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(nUser, newRefreshToken);

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



        private void SetRefreshToken(UserDTO user, RefreshToken newRefreshToken)
        {

            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                var cookiesOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.Expires,
                };

                context.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookiesOptions);
            }
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _db.SaveChanges();
        }

        public async Task<List<UserDTO>> RegisterUser(UserDTO userDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);


            if (user != null)
            {
                return new List<UserDTO> { new UserDTO { ErrorMessage = "User already exists" } };
            }
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
