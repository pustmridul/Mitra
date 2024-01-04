using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ResponseDto _rsponseDTO;
        private readonly IUserService _user;

        public AuthController(IUserService user)
        {
            _user = user;
            _rsponseDTO = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> RegisterUser(UserDTO userDto)
        {
            try
            {
                var user = await _user.RegisterUser(userDto);
                _rsponseDTO.Result = user;
            }
            catch (Exception ex)
            {
                _rsponseDTO.IsSuccess = false;
                _rsponseDTO.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _rsponseDTO;


        }
   

        [HttpPost("login")]
        public async Task<ActionResult<object>> login(UserDTO userDto)
        {
            try
            {
                
                 var login = await _user.Login(userDto);
                _rsponseDTO.Result = login; 
            }
            catch (Exception ex) 
            { 
                _rsponseDTO.IsSuccess = false;
            }    
            return _rsponseDTO;
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refresh = Request.Cookies["refreshToken"];
            var token = await _user.RefreashToken(refresh);
           

            return Ok(token);

        }

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddDays(7)
        //    };
        //    return refreshToken;
        //}

        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookiesOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires,
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookiesOptions);
        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenCreated = newRefreshToken.Expires;
        //}





    }
}
