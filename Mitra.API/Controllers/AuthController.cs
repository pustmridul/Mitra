﻿using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos.Common;
using Mitra.Services.Dtos.User;
using Mitra.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cors;

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
        public async Task<ActionResult<object>> login(LoginDto userDto)
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
        
        [HttpGet("google-login")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
       
        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallbackAsync(string returnUrl = "/")
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return BadRequest("Failed to authenticate with Google.");
            }

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

            // Further processing - create user account, sign in, etc.

            return Ok(new { Email = email, Name = name });
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
