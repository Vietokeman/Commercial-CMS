﻿using CMS.Core.SeedWorks;
using CMS.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        [Route("posts")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("posts/{categorySlug}")]
        public async Task<IActionResult> ListByCategory([FromRoute] string categorySlug, [FromQuery] int page = 1)
        {
            var posts = await _unitOfWork.Posts.GetPostsByCategoryPaging(categorySlug, page);
            var category = await _unitOfWork.PostCategories.GetBySlug(categorySlug);

            if (category == null || posts == null)
            {
                return NotFound(); // hoặc return về trang lỗi
            }

            var viewModel = new PostsListByCategoryViewModel
            {
                Posts = posts,
                Category = category,
            };
            return View(viewModel);
        }


        [Route("tags/{tagSlug}")]
        public IActionResult ListByTag([FromRoute] string categorySlug, [FromQuery] int? page = 1)
        {
            return View();
        }

        [Route("post/{slug}")]
        public async Task<IActionResult> Details([FromRoute] string slug)
        {
            var posts = await _unitOfWork.Posts.GetBySlug(slug);
            var category = await _unitOfWork.PostCategories.GetBySlug(posts.CategorySlug);

            var viewModel = new PostDetailViewModel
            {
                Post = posts,
                Category = category,
            };
            return View(viewModel);
        }
    }
}