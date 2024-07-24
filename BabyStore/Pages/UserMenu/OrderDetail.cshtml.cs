using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using BOs.Entities;

namespace BabyStore.Pages.UserMenu
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;

        public OrderDetailModel(IOrderDetailService orderDetailService, IOrderService orderService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
        }

        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public void OnGet(string id) // Ensure the parameter type matches the ID type
        {
            Order = _orderService.GetOrderById(id); // Adjust method if necessary
            if (Order != null)
            {
                OrderDetails = _orderDetailService.GetOrderDetails(Order.OrderId);
            }
        }
    }
}
