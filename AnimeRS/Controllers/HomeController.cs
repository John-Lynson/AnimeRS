using AnimeRS.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public HomeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public IActionResult Index()
        {
            var animes = _animeRepository.GetAllAnimes();
            return View(animes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ... andere acties ...
    }
}
