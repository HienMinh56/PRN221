using BOs.Entities;
using Repos;
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
        private readonly OrderRepo orderRepo;

        public OrderService()
        {
            orderRepo = new OrderRepo();
        }

        public async Task AddOrder(Order order)
        {
            await orderRepo.AddOrder(order);
        }

        public Order GetOrder(string OrderId)
        {
            return orderRepo.GetOrder(OrderId);
        }

        public List<Order> GetOrderbyUserId(string userId)
        {
            return orderRepo.GetOrderbyUserId(userId);
        }

        public List<Order> GetOrders()
        {
            return orderRepo.GetOrders();
        }
    }
}
