using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Discounts;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Interfaces.Discounts;
using TwistFood.Service.Interfaces.Products;

namespace TwistFood.Api.Controllers.Discounts
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            this._discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _discountService.GetAllAsync(new PagenationParams(page)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _discountService.GetAsync(id));

        [HttpDelete("{id}"), Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
            => Ok(await _discountService.DeleteAsync(id));

        [HttpPost, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> CreateAsync([FromForm] DiscountCreateDto dto)
            => Ok(await _discountService.CreateDiscountAsync(dto));

        [HttpPut, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] DiscountUpdateDto dto)
            => Ok(await _discountService.UpdateAsync(id, dto));
    }
}
