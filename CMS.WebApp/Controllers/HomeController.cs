using CMS.Core.SeedWorks;
using CMS.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IActionResult> Index()
        {
            //var viewModel = new HomeViewModel();
            //viewModel.LatestPosts = await _unitOfWork.Posts.GetLastestPublishPost(5);
            var viewModel = new HomeViewModel()
            {
                LatestPosts = await _unitOfWork.Posts.GetLastestPublishPost(5)

            };
            Console.WriteLine(viewModel);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
