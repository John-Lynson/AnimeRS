using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using System.Security.Claims;
using AnimeRS.Core.ViewModels;

namespace AnimeRS.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly AnimeService _animeService;
        private readonly ReviewService _reviewService;
        private readonly FavoriteAnimeService _favoriteAnimeService;
        private readonly AnimeLoverService _animeLoverService;

        public SearchController(AnimeService animeService, ReviewService reviewService, FavoriteAnimeService favoriteAnimeService, AnimeLoverService animeLoverService)
        {
            _animeService = animeService;
            _reviewService = reviewService;
            _favoriteAnimeService= favoriteAnimeService;
            _animeLoverService= animeLoverService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("anime/{id}")]
        public IActionResult AnimeDetails(int id)
        {
            var anime = _animeService.GetAnimeById(id);
            var reviews = _reviewService.GetReviewsByAnimeId(id);

            if (anime == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isFavorite = false;
            if (!string.IsNullOrEmpty(userId))
            {
                // Gebruik de AnimeLoverService om de AnimeLover te krijgen op basis van Auth0UserId
                var animeLover = _animeLoverService.GetByAuth0UserId(userId);
                if (animeLover != null)
                {
                    // Controleer of de anime een favoriet is van de gebruiker
                    isFavorite = _favoriteAnimeService.GetFavoriteAnimesByAnimeLoverId(animeLover.Id)
                        .Any(fa => fa.AnimeId == id);
                }
            }

            double averageScore = _reviewService.GetAverageScore(id); 

            var viewModel = new AnimeDetailsViewModel
            {
                Anime = anime,
                Reviews = reviews ?? new List<Review>(),
                FavoriteAnime = isFavorite,
                AverageScore = averageScore // Voeg dit toe aan uw ViewModel
            };

            return View(viewModel);
        }
    }
}
