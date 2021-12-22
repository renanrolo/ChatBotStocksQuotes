using ChatBotStocksQuotes.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserTokenResponse>> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);

                return BadRequest(new { errors });
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                false);

            if (result.Succeeded)
            {
                return await BuildToken(model);
            }

            return BadRequest(new { errors = new string[] { "Inválid Login" } });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserTokenResponse>> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                return BadRequest(new { errors });
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return await BuildToken(model);
            }

            return BadRequest(new { errors = result.Errors.Select(x => x.Description) });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        private async Task<UserTokenResponse> BuildToken(UserIdentificationBase userIdentification)
        {
            var user = await _userManager.FindByEmailAsync(userIdentification.Email);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userIdentification.Email),
                new Claim(JwtRegisteredClaimNames.Email, userIdentification.Email),
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenExpirationInHours = Convert.ToInt32(_configuration["Auth:TokenExpirationInHours"]);

            var expiration = DateTime.UtcNow.AddHours(tokenExpirationInHours);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Email = user.Email,
                Id = user.Id
            };
        }
    }
}
