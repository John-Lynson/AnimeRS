using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ... andere acties
    }
}
