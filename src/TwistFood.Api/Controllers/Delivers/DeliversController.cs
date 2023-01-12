using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Interfaces.Delivers;

namespace TwistFood.Api.Controllers.Delivers
{
    [Route("api/delivers")]
    [ApiController]
    public class DeliversController : ControllerBase
    {
        private readonly IDeliverRegisterService _deliverRegisterService;

        public DeliversController(IDeliverRegisterService deliverRegisterService)
        {
            this._deliverRegisterService = deliverRegisterService;
        }

        [HttpPost("register"), Authorize(Roles = "head")]
        public async Task<IActionResult> RegisterAsync([FromForm] DeliverRegistrDto dto)
            => Ok(await _deliverRegisterService.DeliverRegisterAsync(dto));
    }
}
