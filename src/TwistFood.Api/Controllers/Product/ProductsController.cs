using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.Operators;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Interfaces.Products;

namespace TwistFood.Api.Controllers.Product
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductsDto dto)
            => Ok(await _productService.CreateProductAsync(dto));
    }
}
