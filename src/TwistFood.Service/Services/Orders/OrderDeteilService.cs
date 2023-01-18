using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Interfaces.Orders;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Entities.Users;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Common.Helpers;
using TwistFood.Service.Dtos.Orders;
using TwistFood.Service.Interfaces.Orders;

namespace TwistFood.Service.Services.Orders
{
    public class OrderDeteilService : IOrderDeteilsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;

        public OrderDeteilService(IUnitOfWork unitOfWork, IOrderService orderService)
        {
            this._unitOfWork = unitOfWork;
            this._orderService = orderService;
        }

        public async Task<bool> OrderCreateAsync(long OrderId,OrderDeteilsCreateDto orderDeteilsDto)
        {
            var IsUser = HttpContextHelper.IsUser;
            var order = await _unitOfWork.Orders.FindByIdAsync(OrderId);  
            if (order is null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }
            if (IsUser)
            {
                if (order.UserId != HttpContextHelper.UserId) { throw new StatusCodeException(HttpStatusCode.Unauthorized, "Unauthorized"); }
            }
            _unitOfWork.Entry(order).State= EntityState.Detached;   
            OrderDetail orderdetail = new OrderDetail() {OrderId = order.Id };
            
            var product = await _unitOfWork.Products.FindByIdAsync(orderDeteilsDto.ProductId);  
            if (product == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found"); }

            orderdetail.ProductId = orderDeteilsDto.ProductId;
            orderdetail.Amount = orderDeteilsDto.Amount;
            orderdetail.Price= orderDeteilsDto.Price;
            order.TotalSum += orderdetail.Price;

            _unitOfWork.Orders.Update(order.Id, order);
            _unitOfWork.OrderDetails.Add(orderdetail);  
            await _unitOfWork.SaveChangesAsync();   
            return true;

        }

        public async Task<bool> OrderDeleteAsync(long id)
        {
            var orderDetail = await _unitOfWork.OrderDetails.FindByIdAsync(id);
            if (orderDetail == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order detail not found"); }
            var order = await _unitOfWork.Orders.FindByIdAsync(orderDetail.OrderId);
            if (order == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }
            _unitOfWork.Entry(order).State = EntityState.Detached;
            if (HttpContextHelper.IsUser)
            {
                if (order.UserId != HttpContextHelper.UserId) { throw new StatusCodeException(HttpStatusCode.Unauthorized, "Unauthorized"); }
            }
            order.TotalSum-= orderDetail.Price; 
            _unitOfWork.Orders.Update(order.Id,order);
            _unitOfWork.OrderDetails.Delete(id);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> OrderUpdateAsync(OrderDetailUpdateDto dto)
        {
            var orderDetail = await _unitOfWork.OrderDetails.FindByIdAsync(dto.OrderDetailId);
            if (orderDetail == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order detail not found"); }
           

            _unitOfWork.Entry(orderDetail).State = EntityState.Detached;

            var order = await _unitOfWork.Orders.FindByIdAsync(orderDetail.OrderId);
            if (order == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }
            if (HttpContextHelper.IsUser)
            {
                if (order.UserId != HttpContextHelper.UserId) { throw new StatusCodeException(HttpStatusCode.Unauthorized, "Unauthorized"); }
            }
            _unitOfWork.Entry(order).State = EntityState.Detached;
           
                
            if (dto.ProductId is not null)
            {
                var product = await _unitOfWork.Products.FindByIdAsync((long)dto.ProductId);
                if (product == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found"); }
                orderDetail.ProductId = product.Id; 
            }

            if (dto.Amount is not null )
            {
                orderDetail.Amount = (int)dto.Amount;
            }
            if(dto.Price is not null)
            {
                orderDetail.Price = (double)dto.Price; 
            }

           await  _orderService.OrderUpdateAsync(new OrderUpdateDto() 
           { OrderId = order.Id, 
             TotalSum = order.TotalSum + orderDetail.Price });  
             
            _unitOfWork.OrderDetails.Update(orderDetail.Id, orderDetail);
                
                
            
           await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
