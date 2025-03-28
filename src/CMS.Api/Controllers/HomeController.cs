using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
