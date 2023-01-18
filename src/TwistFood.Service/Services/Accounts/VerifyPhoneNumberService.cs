
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Service.Services.Accounts
{
    public class VerifyPhoneNumberService : IVerifyPhoneNumberService
    {
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWork _context;
        private readonly IAuthManager _authManager;

        public VerifyPhoneNumberService(IMemoryCache cache, IUnitOfWork unitOfWork, IAuthManager authManager)
        {
            _cache = cache;
            _context = unitOfWork;
            _authManager = authManager;
        }
        public async Task<bool> SendCodeAsync(SendToPhoneNumberDto sendToPhoneNumberDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.PhoneNumber== sendToPhoneNumberDto.PhoneNumber);
            if (user is null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found"); }
            Random r = new Random();
            int code = r.Next(1000, 9999);

            _cache.Set(sendToPhoneNumberDto.PhoneNumber, code, TimeSpan.FromMinutes(2));

            var client = new RestClient("https://sms.sysdc.ru/index.php");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("mobile_phone", sendToPhoneNumberDto.PhoneNumber);
            request.AddParameter("message", $"TwistFood Tadiqlash kodi: {code}");
            request.AddHeader("Authorization", "Bearer oILdj1OUmUqDZWXanwPIR3vFYVh7kDiDb6fFzIHh2OsBMeCM8eIbsadZCtn1ZgON");
            IRestResponse response = client.Execute(request);
            if (response.Content.ToString().Substring(11, 5) == "error")
            {
                throw new StatusCodeException(HttpStatusCode.Forbidden, "We are unable to send messages to this company at this time");
            }
            return true;
        }

        public async Task<string> VerifyPhoneNumber(VerifyPhoneNumberDto verifyPhoneNumberDto)
        {
            if (_cache.TryGetValue(verifyPhoneNumberDto.PhoneNumber, out int exceptedCode))
            {
                if (exceptedCode != verifyPhoneNumberDto.Code)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is wrong!");

                else
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == verifyPhoneNumberDto.PhoneNumber);
                    if (user is null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found"); }

                    return _authManager.GenerateUserToken(user); 
                }
            }
            else
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is expired");
        }
    }
}
