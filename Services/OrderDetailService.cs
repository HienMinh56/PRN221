using Repos.Interfaces;
using Repos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;

namespace Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetail = null;
        public OrderDetailService()
        {
            if (_orderDetail == null)
            {
                _orderDetail = new OrderDetailRepository();
            }
        }

        public List<OrderDetail> GetOrderDetails(string orderId)
        {
            return _orderDetail.GetOrderDetails(orderId);
        }
    }
}
