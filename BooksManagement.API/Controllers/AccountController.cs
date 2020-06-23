using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

    }
}