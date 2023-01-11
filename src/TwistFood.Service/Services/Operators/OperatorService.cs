using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Users;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Operators;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Operators;
using TwistFood.Service.Security;

namespace TwistFood.Service.Services.Operators
{
    public class OperatorService : IOperatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IAuthManager _authManager;

        public OperatorService(IUnitOfWork unitOfWork, IFileService fileService, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _authManager = authManager;
        }

        public async Task<string> OperatorLoginAsync(OperatorLoginDto operatorLoginDto)
        {
            var res = await _unitOfWork.Operators.FirstOrDefaultAsync(x => x.Email == operatorLoginDto.Email);
            if (res == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Operator not found, Email is incorrect!"); }

            if (PasswordHasher.Verify(operatorLoginDto.Password, res.Salt, res.PasswordHash))
                return _authManager.GenerateOperatorToken(res);

            throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Password is wrong");

        }

        public async Task<bool> OperatorRegisterAsync(OperatorRegisterDto operatorRegisterDto)
        {
            var res = await _unitOfWork.Operators.FirstOrDefaultAsync(x => x.Email == operatorRegisterDto.Email);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Operator is already exist");

            Operator @operator = (Operator)operatorRegisterDto;

            if (operatorRegisterDto.Image is not null)
            {
                @operator.ImagePath = await _fileService.SaveImageAsync(operatorRegisterDto.Image);
            }

            var hashResult = PasswordHasher.Hash(operatorRegisterDto.Password);

            @operator.Salt = hashResult.Salt;

            @operator.PasswordHash = hashResult.Hash;

            _unitOfWork.Operators.Add(@operator);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}
