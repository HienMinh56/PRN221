using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface IOrderRepo
    {
        public List<Order> GetOrders();

        public Order GetOrder(string OrderId);

        public Task AddOrder(Order order);
    }
}
