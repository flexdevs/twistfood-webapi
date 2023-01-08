using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Interfaces.Delivers;

namespace TwistFood.Api.Controllers.Delivers
{
    [Route("api/delivers")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IDeliverRegisterService _deliverRegisterService;

        public AccountsController(IDeliverRegisterService deliverRegisterService)
        {
            this._deliverRegisterService = deliverRegisterService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] DeliverRegistrDto dto)
            => Ok(await _deliverRegisterService.DeliverRegisterAsync(dto));
    }
}
