
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
using TwistFood.Service.Dtos.AccountAdmin;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Dtos.Operators;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Admins;
using TwistFood.Service.Security;

namespace TwistFood.Service.Services.Admins
{
    public class AdminService : IAdminService
    {
        private IUnitOfWork _unitOfWork;
        private IAuthManager _authManager;
        private IFileService _fileService;

        public AdminService(IUnitOfWork unitOfWork, 
                            IAuthManager authManager,
                            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            _fileService = fileService;
        }

        public async Task<string> AdminLoginAsync(AdminLoginDto adminLoginDto)
        {
            var res = await _unitOfWork.Admins.FirstOrDefaultAsync(x => x.Email == adminLoginDto.Email);
            if (res == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Admin not found, Email is incorrect!"); }

            if (PasswordHasher.Verify(adminLoginDto.Password, res.Salt, res.PasswordHash))
                return _authManager.GenerateAdminToken(res);

            throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Password is wrong");
        }

        public async Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto)
        {
            var res = await _unitOfWork.Admins.FirstOrDefaultAsync(x => x.Email == adminRegisterDto.Email);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Operator is already exist");

            Admin admin = (Admin)adminRegisterDto;

            if (adminRegisterDto.Image is not null)
            {
                admin.ImagePath = await _fileService.SaveImageAsync(adminRegisterDto.Image);
            }

            var hashResult = PasswordHasher.Hash(adminRegisterDto.Password);

            admin.Salt = hashResult.Salt;

            admin.PasswordHash = hashResult.Hash;

            _unitOfWork.Admins.Add(admin);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}
