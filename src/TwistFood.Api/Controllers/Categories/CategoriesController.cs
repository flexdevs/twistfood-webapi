using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Interfaces.Categories;
using TwistFood.Service.Interfaces.Delivers;

namespace TwistFood.Api.Controllers.Categories
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpPost, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> RegisterAsync([FromForm] CategoryDto dto)
            => Ok(await _categoryService.CreateCategoryAsync(dto));

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _categoryService.GetAllAsync(new PagenationParams(page)));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _categoryService.GetAsync(id));

        [HttpDelete("{id}"), Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _categoryService.DeleteAsync(id));
    }
}
