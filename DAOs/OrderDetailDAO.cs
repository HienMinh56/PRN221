using BOs;
using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class OrderDetailDAO
    {
        private readonly Dbprn221Context _context;
        private static OrderDetailDAO instance = null;

        public static OrderDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }

        public OrderDetailDAO()
        {
            _context = new Dbprn221Context();
        }
        public List<OrderDetail> GetOrderDetails(string orderId)
        {
            return _context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
        }

    }
}
