using AnimeRS.Data.Repositories;
using AnimeRS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimeRS.Web.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeController (IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var animes = await _animeRepository.GetAllAsync();
            return View(animes);
        }
    }
}
