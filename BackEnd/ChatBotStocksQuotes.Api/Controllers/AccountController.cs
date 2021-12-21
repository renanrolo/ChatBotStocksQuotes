using ChatBotStocksQuotes.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Controllers
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);

                return BadRequest(new { errors });
            }

            var result = await signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new { errors = new string[] { "Inválid Login" } });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }

                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new { errors = errors });
            }

            return Ok(model);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> WhoAmI()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var logged = userId != null;

            return Ok(new
            {
                Logged = logged,
                UserId = userId,
                Email = userEmail,
                Message = logged ? $"Hello, {userEmail}!" : "Hello" 
            });
        }
    }
}
