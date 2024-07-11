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

        public Order GetOrder(string OrderId)
        {
            return orderRepo.GetOrder(OrderId);
        }

        public List<Order> GetOrders()
        {
            return orderRepo.GetOrders();
        }
    }
}
