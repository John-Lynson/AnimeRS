using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
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
    }
}