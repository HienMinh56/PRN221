using BOs.Entities;
using DAOs;
using Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderDAO _orderDAO= null;
        public OrderRepo()
        {
            if (_orderDAO == null)
            {
                _orderDAO= new OrderDAO();
            }
        }

        public async Task AddOrder(Order order)
        {
            await OrderDAO.Instance.AddOrder(order);
        }

        public Order GetOrder(string OrderId)
        {
            return OrderDAO.Instance.GetOrder(OrderId);
        }

        public List<Order> GetOrders()
        {
            return OrderDAO.Instance.GetOrders();
        }
    }
}
