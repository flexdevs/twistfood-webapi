using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Enums;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Common.Helpers;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos.Orders;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Interfaces.Orders;
using TwistFood.Service.ViewModels.Orders;

namespace TwistFood.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginatorService _paginatorService;

        public OrderService(IUnitOfWork unitOfWork, IPaginatorService paginatorService)
        {
            _unitOfWork = unitOfWork;
            this._paginatorService = paginatorService;

        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(PagenationParams @params)
        {
            var query = _unitOfWork.Orders.GetAll()
          .OrderByDescending(x => x.Id);

            var res = await _paginatorService.ToPageAsync(query,
                @params.PageNumber, @params.PageSize);
            if (res is null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Orders not found"); }

            List<OrderViewModel> result = new List<OrderViewModel>();   
            foreach (var order in res)
            {
                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    Id = order.Id,  
                    CreatedAt= order.CreatedAt, 
                    paymentType= order.PaymentType.ToString(),
                    Status= order.Status.ToString(),   
                    TotalSum= order.TotalSum, 
                    UpdatedAt= order.UpdatedAt
                };
                var user = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.Id == order.UserId);
                
                if(order.OperatorId is not null)
                {
                    orderViewModel.operatorId = (long)order.OperatorId;
                }

                if (order.DeliverId is not null)
                {
                    orderViewModel.deliverId = (long)order.DeliverId;
                }

                orderViewModel.UserPhoneNumber = user!.PhoneNumber;
                string orderDetails = "";
                var orderd = _unitOfWork.OrderDetails.GetAll(order.Id).AsNoTracking().ToList();
                if(orderd is not null)
                    foreach (var item in orderd)
                    {
                        var pr = await _unitOfWork.Products.FindByIdAsync(item.ProductId);
                        orderDetails += pr!.ProductName + ", ";
                    }

                orderViewModel.OrderDetails = orderDetails; 
                result.Add(orderViewModel); 
            }
            return result;
        }

        public async Task<OrderWithOrderDetailsViewModel> GetOrderWithOrderDetailsAsync(long id)
        {
          var order = await _unitOfWork.Orders.FindByIdAsync(id);
            if (order is null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }
            if (HttpContextHelper.IsUser)
            {
                if (order.UserId != HttpContextHelper.UserId) { throw new StatusCodeException(HttpStatusCode.Unauthorized, "Unauthorized"); }
            }
            OrderWithOrderDetailsViewModel orderDetailsViewModel = new OrderWithOrderDetailsViewModel()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                paymentType = order.PaymentType.ToString(),
                Status = order.Status.ToString(),
                TotalSum = order.TotalSum,
                UpdatedAt = order.UpdatedAt,
            };
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.Id == order.UserId);

            orderDetailsViewModel.UserPhoneNumber = user!.PhoneNumber;

            List<OrderDetailViewModel> list = new List<OrderDetailViewModel>();     
            
            var orderDetails =  _unitOfWork.OrderDetails.GetAll(id).AsNoTracking().ToList();
            foreach (var orderDetail in orderDetails)
            {
                OrderDetailViewModel detailsViewModel = new OrderDetailViewModel()
                {
                    Id = orderDetail.Id,
                    Price = orderDetail.Price,
                };
                
                detailsViewModel.ProductName = (await _unitOfWork.Products.FindByIdAsync(orderDetail.ProductId))!.ProductName;
                
                list.Add(detailsViewModel);
            }
            orderDetailsViewModel.OrderDetails = list;  

            return orderDetailsViewModel;   


        }

        public async Task<long> OrderCreateAsync(OrderCreateDto dto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(HttpContextHelper.UserId);
            if (user == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found"); }
            Location location = new Location() { Latitude = dto.Latitude, Longitude = dto.Longitude }; 
            _unitOfWork.Locations.Add(location);
            await _unitOfWork.SaveChangesAsync();
            var Ilocation = await _unitOfWork.Locations.FirstOrDefaultAsync(x => x.Latitude == location.Latitude && x.Longitude == location.Longitude);
            if (Ilocation == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Location not found"); }

            var order = (Order)dto;
            order.UserId = user.Id;
            order.ILocationId = Ilocation.Id;
            _unitOfWork.Orders.Add(order);
            await _unitOfWork.SaveChangesAsync();

            var returnOrder = await _unitOfWork.Orders.FirstOrDefaultAsync(x => x.UserId == order.UserId && x.CreatedAt == order.CreatedAt);
            if (returnOrder == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }
            return  returnOrder.Id;
        }

        public async Task<bool> OrderUpdateAsync(OrderUpdateDto dto)
        {
            
            var order = await _unitOfWork.Orders.FindByIdAsync(dto.OrderId);
            if (order == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }

            if(dto.Status is not null)
            {
                order.Status = (OrderStatus)dto.Status;
            }

            if (dto.OperatorId is not null)
            {
                var @operator = await _unitOfWork.Operators.FirstOrDefaultAsync(x => x.Id == dto.OperatorId);
                if (@operator == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Operator not found"); }
                order.OperatorId = @operator.Id;
            }
            if (dto.DeliverId is not null)
            {
                var deliver = await _unitOfWork.Delivers.FirstOrDefaultAsync(x => x.Id == dto.DeliverId);
                if (deliver == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Deliver not found"); }
                order.DeliverId = deliver.Id;
            }
            if (dto.DeliveredAt is not null)
            {
                order.DeliveredAt = dto.DeliveredAt.Value.ToUniversalTime();
            }
            if (dto.TotalSum is not null) 
            {
                order.TotalSum = dto.TotalSum.Value;    
            } 
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order.Id, order);
            await _unitOfWork.SaveChangesAsync();

            return true;

        }
    }
}
