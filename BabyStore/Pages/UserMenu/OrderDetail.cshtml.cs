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
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public Order Order { get; set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = _orderService.GetOrderById(id);
            if (Order == null)
            {
                return NotFound();
            }

            OrderDetails = _orderDetailService.GetOrderDetails(id);
            return Page();
        }
    }
}
