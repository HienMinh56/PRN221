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

namespace BabyStore.Pages.Admin.OrderManagement
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private const int PageSize = 8;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<Order> Order { get; set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        [BindProperty]
        public string? OrderId { get; set; }
        [BindProperty]
        public string? UserName { get; set; }
        [BindProperty]
        public int? Status { get; set; }

        public IActionResult OnGet(int? pageIndex, string? orderId, string? userName, int? status)
        {
            OrderId = orderId;
            UserName = userName;
            Status = status;

            var OrderList = _orderService.GetOrders();

            if (!string.IsNullOrEmpty(OrderId))
            {
                OrderList = OrderList.Where(a => a.OrderId.ToUpper().Contains(OrderId.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                OrderList = OrderList.Where(a => a.UserId.ToUpper().Contains(UserName.Trim().ToUpper())).ToList();
            }

            if (Status.HasValue)
            {
                OrderList = OrderList.Where(a => a.Status == Status.Value).ToList();
            }

            PageIndex = pageIndex ?? 1;

            // Paginate the filtered list
            var count = OrderList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = OrderList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Order = items;
            return Page();
        }

        public IActionResult OnPost(int? pageIndex)
        {
            return RedirectToPage(new { pageIndex, orderId = OrderId, userName = UserName, status = Status });
        }
    }
}
