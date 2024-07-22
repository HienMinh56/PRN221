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
        Task<string> CreatePaymentUrl(string userId, decimal amount, string orderId);
        Task<string> Checkout(string userId, decimal totalAmount, List<CartItem> cartItems);
    }
}
