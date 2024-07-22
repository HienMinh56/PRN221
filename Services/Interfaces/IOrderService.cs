
using BOs.Entities;
using BOs.Model.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {

        Task<string> CreateOrder(string userId, decimal totalAmount, List<CartItem> cartItems);
        Task UpdateOrderStatus(string orderId, int status);

        List<Order> GetOrders();
        Order GetOrder(string OrderId);
        List<Order> GetOrderbyUserId(string userId);

    }
}
