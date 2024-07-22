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
        Task AddOrder(Order order);
        Task UpdateOrderStatus(string orderId, int status);
    }
}
