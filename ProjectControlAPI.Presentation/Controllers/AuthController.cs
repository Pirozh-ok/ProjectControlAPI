using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs;
using System.Security.Claims;

namespace ProjectControlAPI.Presentation.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public AuthController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> SignIn([FromQuery] string mail)
        {
            AuthResponseDTO response = await _workerService.SignInAsync(mail);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()),
                new Claim(ClaimTypes.Email, response.Mail),
                new Claim(ClaimTypes.Role, response.Role.ToString())
            };

            var claimIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookies", claimPrincipal);

            return Ok();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok();
        }
    }
}
