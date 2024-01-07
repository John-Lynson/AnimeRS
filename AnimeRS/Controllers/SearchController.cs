using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Interfaces; 
using AnimeRS.Core.Models;
using AnimeRS.Core.ViewModels;
using System.Security.Claims;

namespace AnimeRS.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAnimeService _animeService;
        private readonly IReviewService _reviewService;
        private readonly IFavoriteAnimeService _favoriteAnimeService;
        private readonly IAnimeLoverService _animeLoverService;

        public SearchController(IAnimeService animeService, IReviewService reviewService,
                                IFavoriteAnimeService favoriteAnimeService,
                                IAnimeLoverService animeLoverService)
        {
            _animeService = animeService;
            _reviewService = reviewService;
            _favoriteAnimeService = favoriteAnimeService;
            _animeLoverService = animeLoverService;
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
                var animeLover = _animeLoverService.GetByAuth0UserId(userId);
                if (animeLover != null)
                {
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
