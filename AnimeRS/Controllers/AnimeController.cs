
using AnimeRS.Data.Repositories;
using AnimeRS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Models;

namespace AnimeRS.Web.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public IActionResult Index()
        {
            var animes = _animeRepository.GetAllAnimes();
            return View(animes);
        }

        public IActionResult Details(int id)
        {
            var anime = _animeRepository.GetAnimeById(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        // Andere acties gerelateerd aan het weergeven van anime-informatie...
    }
}