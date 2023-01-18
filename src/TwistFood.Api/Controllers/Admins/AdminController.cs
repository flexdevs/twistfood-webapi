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
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerifyDto email)
        {
            
            return Ok(await _emailService.VerifyEmail(email));
        }

        [HttpPost("send-code-to-email"), AllowAnonymous]
        public async Task<IActionResult> SendToEmail([FromBody] SendCodeToEmailDto email)
        {
            return Ok(await _emailService.SendCodeAsync(email));
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordDto forgotPassword)
        {
            
            return Ok(await _emailService.VerifyPasswordAsync(forgotPassword));
        }
    }
}
