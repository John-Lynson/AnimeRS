using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services; // Zorg ervoor dat u de juiste namespaces importeert

namespace AnimeRS.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly AnimeService _animeService;

        public SearchController(AnimeService animeService)
        {
            _animeService = animeService;
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
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }
    }
}
