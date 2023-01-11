using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Api.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISendToPhoneNumberService _sendToPhoneNumberService;

        public AccountsController(IAccountService accountService, 
            ISendToPhoneNumberService sendToPhoneNumberService)
        {
            this._accountService = accountService;
            this._sendToPhoneNumberService = sendToPhoneNumberService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] AccountRegisterDto dto)
            => Ok(await _accountService.AccountRegisterAsync(dto));

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto dto)
            => Ok(new { Token = await _accountService.AccountLoginAsync(dto) });

        [HttpGet("send-to-phone-number")]
        public async Task<IActionResult> SendCodeAsync([FromQuery] SendToPhoneNumberDto dto)
            => Ok(await _sendToPhoneNumberService.SendCodeAsync(dto));
    }
}
