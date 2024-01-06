using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnimeRS.Core.Models;
using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace AnimeRS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AnimeLoverService _animeLoverService;
        private readonly FavoriteAnimeService _favoriteAnimeService;

        public AccountController(AnimeLoverService animeLoverService, FavoriteAnimeService favoriteAnimeService)
        {
            _animeLoverService = animeLoverService;
            _favoriteAnimeService= favoriteAnimeService;
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
        public async Task<IActionResult> Profile()
        {
            var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity.Name;

            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                animeLover = new AnimeLover
                {
                    Username = username,
                    Role = "User",
                    Auth0UserId = auth0UserId
                };
                _animeLoverService.AddAnimeLover(animeLover);
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
            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                // Maak een nieuwe AnimeLover aan als deze niet bestaat
            }
            else
            {
                // Voeg de rol toe aan de claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, animeLover.Role)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
