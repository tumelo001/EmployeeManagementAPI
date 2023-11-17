using Employee_Management.Configurations;
using Employee_Management.Models;
using Employee_Management.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Employee_Management.Controllers
{
    
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(requestDto.Email);
                if (user != null)
                {
                    var valid = await _userManager.CheckPasswordAsync(user, requestDto.Password);
                    if (valid)
                    {
                        var jwtToken = GenearateJwtToken(user);
                        return Ok(new AuthResult
                        {
                            Token = jwtToken,
                            Result = true
                        });
                    }
                }
                return BadRequest(new AuthResult
                {
                    Result = false,
                    Errors = new List<string> { "Invalid password or email" }
                });
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationRequestDto requestDto)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(requestDto.Email);   
                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        Email = requestDto.Email,
                        UserName = requestDto.UserName
                    }; 

                    var results = await _userManager.CreateAsync(user, requestDto.Password); 

                    if (results.Succeeded)
                    {
                        var token = GenearateJwtToken(user); 

                            return Ok(new AuthResult()
                            {
                                Token = token,
                                Result = true
                            });  
                    }
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>() { "Something went wrong. Please try again later."},
                        Result = false
                    });
                }
                return BadRequest(new AuthResult() 
                {
                    Errors = new List<string>() { "Email already exits"},
                    Result = false
                });

            }
            return BadRequest(ModelState);  
        }

        private string GenearateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtConfig:JwtKey").ToString());

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
               
            };
           
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken; 
        }
    }
}
