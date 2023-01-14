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

        [HttpPost("Create"), AllowAnonymous]
        public async Task<IActionResult> OrderCreateAsync([FromForm] OrderCreateDto dto)
            => Ok(await _orderService.OrderCreateAsync(dto));

        [HttpPut("Update"), AllowAnonymous]
        public async Task<IActionResult> OrderUpdateAsync([FromForm] OrderUpdateDto dto)
            => Ok(await _orderService.OrderUpdateAsync(dto));

        [HttpGet("GetAll"), AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(PagenationParams @params)
            => Ok(await _orderService.GetAllAsync(@params));

        [HttpGet("{Id}"), AllowAnonymous]

        public async Task<IActionResult> GetByIdAsync(long OrderId)
            => Ok(await _orderService.GetOrderWithOrderDetailsAsync(OrderId));
    }
}
