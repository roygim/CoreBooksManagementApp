using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BooksManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._config = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            var user = new IdentityUser
            {
                UserName = "roy",
                Email = "roy@gmail.com"
            };

            var result = await userManager.CreateAsync(user, "1234");

            if (result.Succeeded)
            {
                return Ok(result);
            }

            foreach (var error in result.Errors)
            {
                string err = error.Description;
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserCred userCred)
        {
            try
            {
                var user = await userManager.FindByEmailAsync("roy@gmail.com");
                //Sign user in with username and password from parameters. This code assumes that the emailaddress is being used as the username. 
                var result = await signInManager.CheckPasswordSignInAsync(user, "1234", false);

                if (result.Succeeded)
                {
                    //Generate unique token with user's details
                    var tokenString = await GenerateJSONWebToken(user);

                    //Return Ok with token string as content
                    return Ok(new { token = tokenString });
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            //Hash Security Key Object from the JWT Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Generate list of claims with general and universally recommended claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            //Retreive roles for user and add them to the claims listing
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));
            //Generate final token adding Issuer and Subscriber data, claims, expriation time and Key
            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                , _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
            );

            //Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}