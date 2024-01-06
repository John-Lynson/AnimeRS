using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnimeService _animeService;

        public HomeController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        public IActionResult Index()
        {
            var topRatedAnimes = _animeService.GetTopAnimesByTotalRating();
            return View(topRatedAnimes);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}