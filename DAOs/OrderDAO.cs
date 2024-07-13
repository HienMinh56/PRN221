using BOs;
using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class OrderDAO
    {
        private readonly Dbprn221Context _dbprn221Context;
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
            _dbprn221Context = new Dbprn221Context();
        }

        public List<Order> GetOrders()
        {
            return _dbprn221Context.Orders.ToList();
        }

        public Order GetOrder(string OrderId)
        {
            return _dbprn221Context.Orders.Find(OrderId);
        }

        public async Task AddOrder(Order order)
        {
            try
            {
                _dbprn221Context.Orders.Add(order);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
