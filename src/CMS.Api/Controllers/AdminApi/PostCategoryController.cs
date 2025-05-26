using AutoMapper;
using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMS.Core.SeedWorks.Constants.Permissions;

namespace CMS.Api.Controllers.AdminApi
{
    [Route("api/admin/post-category")]
    [ApiController]
    public class PostCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(PostCategories.View)]
        public async Task<ActionResult<List<PostCategoryDto>>> GetPostCategories()
        {
            var postCategories = await _unitOfWork.PostCategories.GetAllAsync();
            var result = _mapper.Map<List<PostCategoryDto>>(postCategories);
            return Ok(result);
        }


        [HttpGet("paging")]
        [Authorize(PostCategories.View)]

        public async Task<ActionResult<PageResult<PostCategoryDto>>> GetPostCategoriesPaging(string? keyword, int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.PostCategories.GetPostCategorysPagingAsync(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(PostCategories.Edit)]

        public async Task<IActionResult> UpdatePostCategory(Guid id, [FromBody] CreateUpdatePostCategoryRequest request)
        {
            var postCategory = await _unitOfWork.PostCategories.GetByIdAsync(id);
            if (postCategory == null)
            {
                return BadRequest("Not found post category");
            }

            _mapper.Map(request, postCategory);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPost]
        [Authorize(PostCategories.View)]
        public async Task<IActionResult> CreatePostCategory([FromBody] CreateUpdatePostCategoryRequest request)
        {
            var postCategory = _mapper.Map<CreateUpdatePostCategoryRequest, PostCategory>(request);

            await _unitOfWork.PostCategories.Add(postCategory);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpDelete("{ids}")]
        [Authorize(PostCategories.Delete)]

        public async Task<IActionResult> DeletePostCategories([FromRoute] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var postCategory = await _unitOfWork.PostCategories.GetByIdAsync(id);
                if (postCategory == null)
                {
                    return NotFound();
                }
                _unitOfWork.PostCategories.Remove(postCategory);
            }
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
    }
}
