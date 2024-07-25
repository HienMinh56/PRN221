using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;
using Services;
using System.Drawing.Printing;

namespace BabyStore.Pages.Admin.OrderManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private const int PageSize = 8;

        public DetailsModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
        }

        public Order Order { get; set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }


        public IActionResult OnGet(int? pageIndex, string? id)
        {
            var OrderItem = _orderService.GetOrderById(id);
            var OrderDetailsList = _orderDetailService.GetOrderDetails(id);
            PageIndex = pageIndex ?? 1;

            // Paginate the list
            var count = OrderDetailsList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = OrderDetailsList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            OrderDetails = items;
            Order = OrderItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id, int status)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await _orderService.UpdateOrderStatus(id, status);
                TempData["message"] = "Update Status Successful";
                TempData["messageType"] = "success";
                return RedirectToPage("./Order");
            }
            catch (Exception ex)
            {
                TempData["message"] = "Update Status Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("./Order");
            }            
        }
    }
}