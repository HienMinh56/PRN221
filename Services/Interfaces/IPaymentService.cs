using BOs.Model.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentUrl(string userId, int amount, string orderId);
        Task<string> Checkout(string userId, int totalAmount, List<CartItem> cartItems);
    }
}
