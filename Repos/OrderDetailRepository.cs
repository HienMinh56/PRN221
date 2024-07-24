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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDetailDAO _orderDAO;

        public OrderDetailRepository()
        {
            _orderDAO = new OrderDetailDAO();
        }

        public List<OrderDetail> GetOrderDetails(string orderId)
        {
            return _orderDAO.GetOrderDetails(orderId);
        }
    }
}
