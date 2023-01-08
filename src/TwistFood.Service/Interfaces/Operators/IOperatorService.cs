using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Operators;

namespace TwistFood.Service.Interfaces.Operators
{
    public interface IOperatorService
    {
        public Task<string> OperatorLoginAsync(OperatorLoginDto operatorLoginDto);
        public Task<bool> OperatorRegisterAsync(OperatorRegisterDto operatorRegisterDto);
    }
}
