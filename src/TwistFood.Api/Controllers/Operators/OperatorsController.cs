using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos;
using TwistFood.Service.Interfaces.Operators;
using TwistFood.Service.Dtos.Operators;

namespace TwistFood.Api.Controllers.Operators
{
    [Route("api/operators")]
    [ApiController]
    public class OperatorsController : ControllerBase
    {
        private readonly IOperatorService _operatorService;

        public OperatorsController(IOperatorService operatorService)
        {
            this._operatorService = operatorService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] OperatorRegisterDto dto)
            => Ok(await _operatorService.OperatorRegisterAsync(dto));

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] OperatorLoginDto dto)
            => Ok(new { Token = await _operatorService.OperatorLoginAsync(dto) });
    }
}
