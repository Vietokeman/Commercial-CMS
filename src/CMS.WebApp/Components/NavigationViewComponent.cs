using CMS.Core.Repositories;
using CMS.Core.SeedWorks;
using CMS.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebApp.Components
{

    public class NavigationViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public NavigationViewComponent(IPostCategoryRepository postCategoryRepo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _unitOfWork.PostCategories.GetAllAsync();
            var navItems = model.Select(x => new NavigationItemViewModel
            {
                Slug = x.Slug,
                Name = x.Name,
                Children = model.Where(y => y.ParentId == x.Id).Select(y => new NavigationItemViewModel
                {
                    Slug = y.Slug,
                    Name = y.Name
                }).ToList()
            }).Where(x => x.Children.Any() || !string.IsNullOrEmpty(x.Slug)).ToList();

            return View(navItems);
        }
    }

}