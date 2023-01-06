using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Accounts;

namespace TwistFood.Service.Interfaces.Accounts
{
    public interface IVerifyEmailService
    {
        Task SendCodeAsync(SendCodeToEmailDto sendCodeToEmailDto);

        Task VerifyEmail(EmailVerifyDto emailVerifyDto);

        Task VerifyPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
