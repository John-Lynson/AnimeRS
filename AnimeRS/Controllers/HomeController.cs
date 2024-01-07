using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimeService _animeService;

        public HomeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        public IActionResult Index()
        {
            var topRatedAnimes = _animeService.GetTopAnimesByTotalRating(5);
            return View(topRatedAnimes);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}