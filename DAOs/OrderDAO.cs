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
            return _context.Orders.Where(o => o.UserId==userId).OrderByDescending(o=>o.Id).ToList();
        }
        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(o => o.Id).Include(o => o.User).ToList();
        }
        public Order GetOrderById(string orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
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
            if (order != null && order.Status !=3 )
            {
                order.Status = status;
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        public async Task CancelOrder()
        {
            var twelveHoursAgo = DateTime.Now.AddHours(-12);
            
            var orders = _context.Orders
                                 .Where(o => o.Status == 2 && o.CreatedDate < twelveHoursAgo)
                                 .ToList();

            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    order.Status = 3; 
                }

                await _context.SaveChangesAsync(); 
            }
        }
        public async Task<string> GenerateOrderId()
        {
            var lastOrder = await _context.Orders.OrderByDescending(o => o.Id).FirstOrDefaultAsync();

            if (lastOrder == null || !lastOrder.OrderId.StartsWith("ORDER"))
            {
                return "ORDER0001";
            }

            string lastOrderId = lastOrder.OrderId;
            int nextIdNumber = int.Parse(lastOrderId.Substring(5)) + 1;
            return "ORDER" + nextIdNumber.ToString("D4");
        }
    }
}
