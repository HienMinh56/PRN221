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

        Task<string> CreateOrder(string userId, int totalAmount, List<CartItem> cartItems, string voucherCode = null);
        Task UpdateOrderStatus(string orderId, int status);

        Task CancelOrder();
        List<Order> GetOrders();
        Order GetOrderById(string OrderId);
        List<Order> GetOrderbyUserId(string userId);


    }
}
