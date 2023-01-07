using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;

namespace TwistFood.Service.Interfaces.Delivers
{
    public interface IDeliverRegisterService
    {
        public Task<bool> DeliverRegisterAsync(DeliverRegistrDto deliverRegistrDto);
    }
}
