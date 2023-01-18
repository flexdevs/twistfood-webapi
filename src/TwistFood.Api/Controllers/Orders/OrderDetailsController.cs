using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.Orders;
using TwistFood.Service.Interfaces.Orders;
using TwistFood.Service.Services.Orders;

namespace TwistFood.Api.Controllers.Orders
{
    [Route("api/order-details")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDeteilsService _orderDetailService;

        public OrderDetailsController(IOrderDeteilsService orderdeltailService)
        {
            this._orderDetailService = orderdeltailService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> OrderCreateAsync(long orderId,[FromBody] OrderDeteilsCreateDto orderDeteilsDto)
            => Ok(await _orderDetailService.OrderCreateAsync(orderId, orderDeteilsDto));


        [HttpPut, AllowAnonymous]
        public async Task<IActionResult> OrderUpdateAsync([FromForm] OrderDetailUpdateDto dto)
            => Ok(await _orderDetailService.OrderUpdateAsync(dto));

        [HttpDelete("{id}"), AllowAnonymous]
        public async Task<IActionResult> DeleteByIdAsync(long id)
            => Ok(await _orderDetailService.OrderDeleteAsync(id));
    }
}
