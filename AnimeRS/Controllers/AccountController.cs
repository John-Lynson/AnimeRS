using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
