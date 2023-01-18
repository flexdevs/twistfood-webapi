using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IVerifyPhoneNumberService _sendToPhoneNumberService;

        public AccountsController(IAccountService accountService, 
            IVerifyPhoneNumberService sendToPhoneNumberService)
        {
            this._accountService = accountService;
            this._sendToPhoneNumberService = sendToPhoneNumberService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] AccountRegisterDto dto)
            => Ok(await _accountService.AccountRegisterAsync(dto));


        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] AccountUpdateDto dto)
            => Ok(await _accountService.AccountUpdateAsync(dto));

        [HttpGet, Authorize(Roles = "head, nohead")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _accountService.GetAllAsync(new PagenationParams(page)));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _accountService.GetAsync(id));

        [HttpGet("send-to-phone-number")]
        public async Task<IActionResult> SendCodeAsync([FromQuery] SendToPhoneNumberDto dto)
            => Ok(await _sendToPhoneNumberService.SendCodeAsync(dto));

        [HttpGet("verify-phone-number")]
        public async Task<IActionResult> VerifyAsync([FromQuery] VerifyPhoneNumberDto dto)
            => Ok(await _sendToPhoneNumberService.VerifyPhoneNumber(dto));
    }
}
