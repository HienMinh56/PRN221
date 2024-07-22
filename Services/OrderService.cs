using BOs.Entities;
using BOs.Model.CartModel;
using Repos;
using Repos.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<string> CreateOrder(string userId, decimal totalAmount, List<CartItem> cartItems)
        {
            var orderId = Guid.NewGuid().ToString();
            var order = new Order
            {
                OrderId = orderId,
                UserId = userId,
                TotalAmount = (int)totalAmount,
                CreatedDate = DateTime.Now,
                Status = 2 // wait
            };

            foreach (var item in cartItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    CreatedDate = DateTime.Now
                });
            }

            await _orderRepo.AddOrder(order);
            return orderId;
        }


        public async Task UpdateOrderStatus(string orderId, int status)
        {
            await _orderRepo.UpdateOrderStatus(orderId, status);
        }

        public List<Order> GetOrderbyUserId(string userId)
        {
            return orderRepo.GetOrderbyUserId(userId);
        }

        public List<Order> GetOrders()

        {
            await _orderRepo.UpdateOrderStatus(orderId, status);
        }
    }
}
