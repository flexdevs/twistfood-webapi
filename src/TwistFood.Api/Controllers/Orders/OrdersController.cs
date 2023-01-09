using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.Orders;
using TwistFood.Service.Interfaces.Operators;
using TwistFood.Service.Interfaces.Orders;

namespace TwistFood.Api.Controllers.Orders
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> OrderCreateAsync([FromForm] OrderCreateDto dto)
            => Ok(await _orderService.OrderCreateAsync(dto));

        [HttpPut("Update")]
        public async Task<IActionResult> OrderUpdateAsync([FromForm] OrderUpdateDto dto)
            => Ok(await _orderService.OrderUpdateAsync(dto));
    }
}
