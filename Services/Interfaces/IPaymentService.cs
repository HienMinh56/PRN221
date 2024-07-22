using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        public interface IPaymentService
        {
            Task<string> CreatePaymentUrl(string userId, decimal amount, string orderId);
        }
    }
}
