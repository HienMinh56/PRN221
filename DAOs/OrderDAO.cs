using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class OrderDAO
    {
        private readonly Dbprn221Context _context;
        private static OrderDAO instance = null;

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }

        public OrderDAO()
        {
            _context = new Dbprn221Context();
        }
        public List<Order> GetOrderbyUserId(string userId)
        {
            return _dbprn221Context.Orders.Where(o => o.UserId==userId).ToList();
        }

        public async Task AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateOrderStatus(string orderId, int status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = status;
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
