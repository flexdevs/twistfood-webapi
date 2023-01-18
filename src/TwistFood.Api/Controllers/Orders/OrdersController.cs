using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Common.Utils;
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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> OrderCreateAsync([FromForm] OrderCreateDto dto)
            => Ok(await _orderService.OrderCreateAsync(dto));

        [HttpPut, AllowAnonymous]
        public async Task<IActionResult> OrderUpdateAsync([FromForm] OrderUpdateDto dto)
            => Ok(await _orderService.OrderUpdateAsync(dto));

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _orderService.GetAllAsync(new PagenationParams(page)));

        [HttpGet("{id}"), AllowAnonymous]

        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _orderService.GetOrderWithOrderDetailsAsync(id));
    }
}
