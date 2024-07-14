
using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrder(string OrderId);
        Task AddOrder(Order order);
    }
}
