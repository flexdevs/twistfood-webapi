
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Service.Services.Accounts
{
    public class SendToPhoneNumberService : ISendToPhoneNumberService
    {
        public async Task<int> SendCodeAsync(SendToPhoneNumberDto sendToPhoneNumberDto)
        {
            Random r = new Random();
            int code = r.Next(1000, 9999);

            var client = new RestClient("https://sms.sysdc.ru/index.php");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("mobile_phone", sendToPhoneNumberDto.PhoneNumber);
            request.AddParameter("message", code);
            request.AddHeader("Authorization", "Bearer oILdj1OUmUqDZWXanwPIR3vFYVh7kDiDb6fFzIHh2OsBMeCM8eIbsadZCtn1ZgON");
            IRestResponse response = client.Execute(request);
            if (response.Content.ToString().Substring(11, 5) == "error")
            {
                throw new StatusCodeException(HttpStatusCode.Forbidden, "We are unable to send messages to this company at this time");
            }
            return code;
        }
    }
}
