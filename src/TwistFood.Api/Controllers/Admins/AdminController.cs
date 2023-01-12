using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.AccountAdmin;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Dtos.Operators;
using TwistFood.Service.Interfaces.Accounts;
using TwistFood.Service.Interfaces.Admins;
using TwistFood.Service.Services.Admins;

namespace TwistFood.Api.Controllers.Admins
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IVerifyEmailService _emailService;
        private readonly IAdminService _adminService;

        public AdminController(IVerifyEmailService emailService,
                                  IAdminService adminService)
        {
            _emailService = emailService;
            _adminService = adminService;
        }

        [HttpPost("register"), Authorize(Roles = "head")]
        public async Task<IActionResult> RegisterAsync([FromForm] AdminRegisterDto dto)
            => Ok(await _adminService.AdminRegisterAsync(dto));

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromForm] AdminLoginDto dto)
            => Ok(new { Token = await _adminService.AdminLoginAsync(dto) });

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
