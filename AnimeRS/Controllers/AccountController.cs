using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnimeRS.Core.Models;
using AnimeRS.Core.ViewModels;
using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace AnimeRS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AnimeLoverService _animeLoverService;
        private readonly FavoriteAnimeService _favoriteAnimeService;
        private readonly ReviewService _reviewService;

        public AccountController(AnimeLoverService animeLoverService, FavoriteAnimeService favoriteAnimeService, ReviewService reviewService)
        {
            _animeLoverService = animeLoverService;
            _favoriteAnimeService= favoriteAnimeService;
            _reviewService=reviewService;
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
            var auth0UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);

            if (animeLover == null)
            {
                return RedirectToAction("Login");
            }

            var favoriteAnimes = _favoriteAnimeService.GetFavoriteAnimesByAnimeLoverId(animeLover.Id);
            var userReviews = _reviewService.GetReviewsByUserId(animeLover.Id);

            var viewModel = new ProfileViewModel
            {
                AnimeLover = animeLover,
                FavoriteAnimes = favoriteAnimes,
                UserReviews = userReviews
            };

            return View(viewModel);
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
