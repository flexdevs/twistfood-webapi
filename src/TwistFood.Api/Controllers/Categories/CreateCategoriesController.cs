using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos;
using TwistFood.Service.Interfaces.Categories;
using TwistFood.Service.Interfaces.Delivers;

namespace TwistFood.Api.Controllers.Categories
{
    [Route("api/categories")]
    [ApiController]
    public class CreateCategoriesController : ControllerBase
    {
        private readonly ICreateCategoryService _createCategoryService;

        public CreateCategoriesController(ICreateCategoryService createCategoryService)
        {
            this._createCategoryService = createCategoryService;
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> RegisterAsync([FromBody] CategoryDto dto)
            => Ok(await _createCategoryService.CreateCategoryAsync(dto));
    }
}
