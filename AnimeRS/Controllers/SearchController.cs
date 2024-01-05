using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;

namespace AnimeRS.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly AnimeService _animeService;
        private readonly ReviewService _reviewService;

        public SearchController(AnimeService animeService, ReviewService reviewService)
        {
            _animeService = animeService;
            _reviewService = reviewService;
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

            var viewModel = new AnimeDetailsViewModel
            {
                Anime = anime,
                Reviews = reviews ?? new List<Review>() // Zorg ervoor dat reviews niet null is
            };

            return View(viewModel);
        }
    }
}
