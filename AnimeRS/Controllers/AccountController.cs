using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnimeRS.Core.Models;
using AnimeRS.Core.Interfaces; // Voeg deze toe
using AnimeRS.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
// Overige using statements...

namespace AnimeRS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AnimeLoverService _animeLoverService;
        private readonly IAnimeLoverRepository _animeLoverRepository;

        public AccountController(AnimeLoverService animeLoverService, IAnimeLoverRepository animeLoverRepository)
        {
            _animeLoverService = animeLoverService;
            _animeLoverRepository = animeLoverRepository;
        }

        // Login actie
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        // Signup actie
        public async Task Signup(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithParameter("screen_hint", "signup")
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity.Name;

            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                animeLover = new AnimeLover(username, "User", auth0UserId);
                _animeLoverService.Create(animeLover);
            }

            return View(animeLover);
        }


        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> PostLogin()
        {
            var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var animeLover = _animeLoverRepository.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                animeLover = new AnimeLover(
                    User.Identity.Name, // Username
                    "User", // Role
                    auth0UserId // Auth0UserId
                );
                _animeLoverRepository.AddAnimeLover(animeLover);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}