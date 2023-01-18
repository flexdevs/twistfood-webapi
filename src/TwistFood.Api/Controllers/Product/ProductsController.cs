using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using TwistFood.Service.Common.Utils;
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

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _productService.GetAllAsync(new PagenationParams(page)));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _productService.GetAsync(id));

        [HttpGet("search/{name}"), AllowAnonymous]
        public async Task<IActionResult> SearchByNameAsync(string name)
            => Ok(await _productService.SearchByNameAsync(name));

        [HttpGet("search"), AllowAnonymous]
        public async Task<IActionResult> ProductForSearchAsync([FromQuery] string? categryName = "", [FromQuery] string? searchname = "")
          => Ok(await _productService.GetAllForSearchAsync(categryName,searchname));

        [HttpDelete("{id}"), Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
            => Ok(await _productService.DeleteAsync(id));

        [HttpPost, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductsDto dto)
            => Ok(await _productService.CreateProductAsync(dto));

        [HttpPut, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] UpdateProductDto dto)
            => Ok(await _productService.UpdateAsync(id, dto));
    }
}
