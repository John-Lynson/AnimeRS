using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
