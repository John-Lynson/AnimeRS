using AnimeRS.Core.Models;
using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AnimeService _animeService;

        public AdminController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        public IActionResult Index()
        {
            var animeDTOs = _animeService.GetAllAnimes();
            var animes = animeDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
            return View(animes);
        }
    }
}
