using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Api.Controllers.Users
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] AccountRegisterDto dto)
            => Ok(await _accountService.AccountRegisterAsync(dto));

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto dto)
            => Ok(new { Token = await _accountService.AccountLoginAsync(dto) });
    }
}
