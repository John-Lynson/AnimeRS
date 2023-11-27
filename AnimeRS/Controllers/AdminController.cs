using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    [Authorize(Roles = "Admin")] // Zorg ervoor dat alleen beheerders toegang hebben
    public class AdminController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public AdminController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public IActionResult Index()
        {
            var animes = _animeRepository.GetAllAnimes();
            return View(animes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Anime anime)
        {
            if (ModelState.IsValid)
            {
                _animeRepository.AddAnime(anime);
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        public IActionResult Edit(int id)
        {
            var anime = _animeRepository.GetAnimeById(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Anime anime)
        {
            if (id != anime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _animeRepository.UpdateAnime(anime);
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        public IActionResult Delete(int id)
        {
            var anime = _animeRepository.GetAnimeById(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _animeRepository.DeleteAnime(id);
            return RedirectToAction(nameof(Index));
        }

        // Andere beheerdersspecifieke acties...
    }
}
