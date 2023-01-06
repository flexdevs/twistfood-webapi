using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Accounts;

namespace TwistFood.Service.Interfaces.Accounts
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMessageDto emailMessageDto);

        Task VerifyPasswordAsync(ResetPasswordDto password);
    }
}
