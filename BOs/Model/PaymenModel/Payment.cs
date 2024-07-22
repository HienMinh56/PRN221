using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Model.PaymenModel
{
    public class Payment
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentUrl { get; set; }
    }
}
