using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnimeRS.Core.Models; // Zorg ervoor dat dit pad overeenkomt met uw AnimeLover model
using Microsoft.AspNetCore.Authentication.Cookies;
// Overige using statements...

namespace AnimeRS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AnimeLoverService _animeLoverService;

        public AccountController(AnimeLoverService animeLoverService)
        {
            _animeLoverService = animeLoverService;
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
            // Veronderstel dat deze waarden uit de gebruikersclaims of een andere bron komen
            var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity.Name;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Aanname: u heeft een methode om te controleren of de gebruiker al bestaat
            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                // Gebruik de constructor om een nieuwe AnimeLover instantie te maken
                animeLover = new AnimeLover(0, username, email, "role", auth0UserId); // Pas de parameters aan volgens uw behoeften
                _animeLoverService.Create(animeLover);
            }

            // Voer hier eventuele extra logica uit...

            return View(animeLover); // Zorg ervoor dat u een passende View heeft
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
    }
}
