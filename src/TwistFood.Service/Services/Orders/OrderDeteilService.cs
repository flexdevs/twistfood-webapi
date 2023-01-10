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
using TwistFood.Domain.Exceptions;
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

        public async Task<bool> OrderCreateAsync(OrderCreateDto orderCreateDto, List<OrderDeteilsCreateDto> orderDeteilsDto)
        {
           var order =  await _orderService.OrderCreateAsync(orderCreateDto);
            foreach (var item in orderDeteilsDto)
            {
                var product = await _unitOfWork.Products.FindByIdAsync(item.ProductId);
                if (product == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found"); }
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    OrderId = order.Id
                    
                };
                orderDetail.Price = product.Price*item.Amount;
                order.TotalSum += orderDetail.Price;
                _unitOfWork.OrderDetails.Add(orderDetail);  
            }
            OrderUpdateDto orderUpdateDto = new OrderUpdateDto() {OrderId = order.Id ,TotalSum = order.TotalSum };
            
            await _orderService.OrderUpdateAsync(orderUpdateDto); 

           await  _unitOfWork.SaveChangesAsync();
            return true;    
        }

        public async Task<bool> OrderUpdateAsync(long orderId,List<OrderDetailUpdateDto> dto)
        {

            var order = _unitOfWork.Orders.FindByIdAsync(orderId);
            if (order == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found"); }

            foreach (var item in dto)
            {
                OrderDetail orderDetail = new OrderDetail();
                if (item.OrderDetailId is not null)
                {
                    var orderdetail = await _unitOfWork.OrderDetails.FindByIdAsync((long)item.OrderDetailId);
                    if (orderdetail == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "order detail not found"); }
                    orderDetail = orderdetail;
                }
                
                if (item.ProductId is not null)
                {
                    var product = _unitOfWork.Products.FindByIdAsync((long)item.ProductId);
                    if (product == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found"); }
                    orderDetail.ProductId = product.Id; 
                }

                if (item.Amount is not null)
                {
                    orderDetail.Amount = (int)item.Amount;
                }

                if (item.OrderDetailId is null)
                {
                    orderDetail.OrderId = orderId;
                    // yangi  yaratish kerak
                    _unitOfWork.OrderDetails.Add(orderDetail);
                }
                else
                {
                    _unitOfWork.OrderDetails.Update(orderId, orderDetail);
                }
                
            }
           await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
