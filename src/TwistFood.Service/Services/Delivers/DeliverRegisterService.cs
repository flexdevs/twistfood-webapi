
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
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Delivers;

namespace TwistFood.Service.Services.Delivers
{
    public class DeliverRegisterService : IDeliverRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public DeliverRegisterService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> DeliverRegisterAsync(DeliverRegistrDto deliverRegistrDto)
        {
            var res = await _unitOfWork.Delivers.FirstOrDefaultAsync(x => x.PhoneNumber == deliverRegistrDto.PhoneNumber);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Deliver is already exist");

            Deliver deliver = (Deliver)deliverRegistrDto;

            if (deliverRegistrDto.Image is not null)
            {
                deliver.ImagePath = await _fileService.SaveImageAsync(deliverRegistrDto.Image);
            }
            
            deliver.BirthDate =(deliver.BirthDate.ToUniversalTime());
            _unitOfWork.Delivers.Add(deliver);
             var result =  await _unitOfWork.SaveChangesAsync();

            return result>0;
        }
    }
}
