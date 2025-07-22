using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;
using CMS.Data.Strategies;
using CMS.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly SearchBySlugStrategy _searchBySlugStrategy;
        private readonly SearchByCategoryStrategy _searchByCategoryStrategy;

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            SearchBySlugStrategy searchBySlugStrategy,
            SearchByCategoryStrategy searchByCategoryStrategy)
        {
            _logger = logger;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _searchBySlugStrategy = searchBySlugStrategy;
            _searchByCategoryStrategy = searchByCategoryStrategy;
        }

        public async Task<IActionResult> Index([FromQuery] SearchInputModel searchInput)
        {
            var posts = new List<PostInListDto>();

            var context = new SearchContext(_searchBySlugStrategy);

            if (!string.IsNullOrWhiteSpace(searchInput?.CategorySlug))
            {
                context.SetStrategy(_searchByCategoryStrategy);
                posts = await context.SearchAsync(searchInput.CategorySlug);
            }
            else if (!string.IsNullOrWhiteSpace(searchInput?.Keyword))
            {
                context.SetStrategy(_searchBySlugStrategy);
                posts = await context.SearchAsync(searchInput.Keyword);
            }
            else
            {
                posts = await _unitOfWork.Posts.GetLastestPublishPost(5);
            }

            var viewModel = new HomeViewModel
            {
                SearchInput = searchInput,
                LatestPosts = posts
            };


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
