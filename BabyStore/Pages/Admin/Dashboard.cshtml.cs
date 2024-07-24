using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using System.Globalization;

namespace BabyStore.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public DashboardModel(IUserService userService,
                              ITransactionService transactionService,
                              IOrderService orderService,
                              IProductService productService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _orderService = orderService;
            _productService = productService;
        }

        public int UserCount { get; private set; }
        public int TransactionCount { get; private set; }
        public int OrderCountSucess { get; private set; }
        public int OrderCount { get; private set; }
        public int Account1 { get; private set; }
        public int Account0 { get; private set; }
        public int ProductCount { get; private set; }
        public int Revenue { get; private set; }
        public string RevenueFormatted { get; private set; }


        private void CalculateDaily()
        {
            var today = DateTime.Today;
            var ordersToday = _orderService.GetOrders()
                                   .Where(order => (order.Status == 1 || order.Status == 4 || order.Status == 5) 
                                   && order.CreatedDate.HasValue 
                                   && order.CreatedDate.Value.Date == today)
                                   .ToList();
            //return ordersToday.Sum(order => order.TotalAmount);
            Revenue = ordersToday.Sum(order => order.TotalAmount);
            OrderCountSucess = ordersToday.Count;
        }


        public IActionResult OnGet()
        {

            UserCount = _userService.GetUsers().Count;
            TransactionCount = _transactionService.GetTransactions().Count;
            OrderCount = _orderService.GetOrders().Count;
            Account1 = _userService.GetUsers().Where(s => s.Status == 1).ToList().Count();
            Account0 = _userService.GetUsers().Where(s => s.Status == 0).ToList().Count();
            ProductCount = _productService.GetProducts().Count;
            CalculateDaily();
            RevenueFormatted = $"{Revenue.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} VND";

            return Page();
        }
    }
}