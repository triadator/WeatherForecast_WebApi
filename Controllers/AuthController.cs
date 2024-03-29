﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Db;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private User AuthenticateUser(User user)
        {
            User _user = null;
            if (user.Login == "admin" && user.Password=="12345")
            {
                _user = new User() { Login = "George" };
            }
            return _user;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],null
                , expires: DateTime.Now.AddHours(2)
                , signingCredentials: credentials);
             return new JwtSecurityTokenHandler().WriteToken(token);           
        }
        [AllowAnonymous]
        [HttpPost]
        //не работает FromHeader
        public IActionResult Login([FromBody] User user)
        {

            IActionResult respons = Unauthorized();
            var _user = AuthenticateUser(user);
            if (_user == null)
            {                
            return StatusCode(401);
            }
            var token = GenerateToken(_user);
            respons = Ok(new { token = token });
            //HttpContext.Response.Cookies.Append("Authorization", $"Bearer {token}");
            return respons;
        }

        
        
    }
}
