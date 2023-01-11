using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Api.Controllers.Admins
{
    [Route("api/admins")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IVerifyEmailService _emailService;

        public AccountsController(IAccountService accountService, IVerifyEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("verify-email"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromForm] EmailVerifyDto email)
        {
            await _emailService.VerifyEmail(email);
            return Ok();
        }

        [HttpPost("send-code-to-email"), AllowAnonymous]
        public async Task<IActionResult> SendToEmail([FromForm] SendCodeToEmailDto email)
        {
            await _emailService.SendCodeAsync(email);
            return Ok();
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromForm] ResetPasswordDto forgotPassword)
        {
            await _emailService.VerifyPasswordAsync(forgotPassword);
            return Ok();
        }
    }
}
