using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;

namespace BabyStore.Pages.UserMenu
{
    public class MyOrderModel : PageModel
    {
        private readonly IOrderService _order;

        public MyOrderModel(IOrderService order)
        {
            _order = order;
        }

        public IList<Order> Orders { get; set; } = new List<Order>();

        public int TotalOrders { get; set; }
        public int TotalAmountDelivered { get; set; }
        public int FilterStatus { get; set; }

        public async Task OnGetAsync(int? status)
        {
            var userId = HttpContext.Session.GetString("id");
            Orders = _order.GetOrderbyUserId(userId);

            if (status.HasValue)
            {
                FilterStatus = status.Value;
                Orders = Orders.Where(o => o.Status == FilterStatus).ToList();
            }

            TotalOrders = Orders.Count;
            TotalAmountDelivered = Orders.Where(o => o.Status == 4).Sum(o => o.TotalAmount);
        }
    }
}
