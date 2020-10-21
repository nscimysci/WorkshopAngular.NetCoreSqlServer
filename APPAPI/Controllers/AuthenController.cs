using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APPAPI.Database;
using APPAPI.Models;
using APPAPI.ViewModels;
using CryptoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;



namespace APPAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        ILogger<AuthenController> _logger;
        public DatabaseContext Context { get; }
        public IConfiguration Config { get; }
        public static IWebHostEnvironment _env;
        public AuthenController(ILogger<AuthenController> logger
        , DatabaseContext Context
        , IConfiguration Config
        , IWebHostEnvironment env)
        {
            this.Config = Config;
            this.Context = Context;
            _logger = logger;
            _env = env;
        }

        [HttpGet("print")]
        public IActionResult Get()
        {
            try
            {
                return Ok("API OK");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            try
            {
                var user = Context.Users.Where(x => x.Username == model.Username).FirstOrDefault();
                if (user == null)
                {
                    return Unauthorized(new { token = "", message = "User not autherize." });
                }

                String tokenString = BuildToken(user);
                return Ok(new { token = tokenString, message = "login successfully" });


            }
            catch (Exception ex)
            {
                _logger.LogError($"Log Login: {ex}");
                return StatusCode(500, new { result = "Error", message = ex });
            }
        }


        private string BuildToken(Users user)
        {

            var claims = new[] {
                new Claim (JwtRegisteredClaimNames.Sub, Config["Jwt:Subject"]),
                new Claim ("id", user.Id.ToString ()),
                new Claim ("username", user.Username),
                new Claim ("role", user.Roleid.ToString ()),
                // for testing [Authorize(Roles = "admin")]
                // new Claim("role", "admin"),
                // new Claim(ClaimTypes.Role, user.Position)
            };

            // AddDay จะเอาเป็น ขนาดไหน แล้วแต่ Set
            var expires = DateTime.Now.AddDays(Convert.ToDouble(Config["Jwt:ExpireDay"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}