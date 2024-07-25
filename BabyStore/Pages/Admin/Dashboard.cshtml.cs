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
        public int ProductCount { get; private set; }
        public int Revenue { get; private set; }
        public string RevenueFormatted { get; private set; }
        public List<string> RevenueDates { get; private set; } = new List<string>();
        public List<decimal> RevenueAmounts { get; private set; } = new List<decimal>();
        public List<int> OrderStatusCounts { get; private set; } = new List<int>();


        private void CalculateAmountDaily()
        {
            var today = DateTime.Today;
            var ordersToday = _orderService.GetOrders()
                                   .Where(order => (order.Status == 1 || order.Status == 4 || order.Status == 5) 
                                        && order.CreatedDate.HasValue 
                                        && order.CreatedDate.Value.Date == today)
                                   .ToList();
            Revenue = ordersToday.Sum(order => order.TotalAmount);
        }

        private void CalculateRevenueByDate(int days)
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-days);
            var allDates = Enumerable.Range(0, days + 1)
                                     .Select(offset => startDate.AddDays(offset))
                                     .ToList();

            var revenues = _orderService.GetOrders()
                                        .Where(order => (order.Status == 1 || order.Status == 4 || order.Status == 5)
                                            && order.CreatedDate.HasValue
                                            && order.CreatedDate.Value.Date >= startDate
                                            && order.CreatedDate.Value.Date <= endDate)
                                        .GroupBy(order => order.CreatedDate.Value.Date)
                                        .Select(group => new
                                        {
                                            Date = group.Key,
                                            TotalAmount = group.Sum(order => order.TotalAmount)
                                        })
                                        .ToList();

            var revenueDict = revenues.ToDictionary(r => r.Date, r => r.TotalAmount);

            foreach (var date in allDates)
            {
                RevenueDates.Add(date.ToString("dd/MM/yyyy"));
                RevenueAmounts.Add(revenueDict.ContainsKey(date) ? revenueDict[date] : 0);
            }
        }

        private void CalculateOrderStatusCounts()
        {
            var statuses = _orderService.GetOrders()
                                        .GroupBy(order => order.Status)
                                        .Select(group => new
                                        {
                                            Status = group.Key,
                                            Count = group.Count()
                                        })
                                        .ToList();

            OrderStatusCounts = new List<int> { 0, 0, 0, 0, 0 };

            foreach (var status in statuses)
            {
                if (status.Status >= 1 && status.Status <= 5)
                {
                    OrderStatusCounts[status.Status - 1] = status.Count;
                }
            }
        }
        private void TotalOrderAndTransactionByDaily()
        {
            var today = DateTime.Today;

            OrderCount = _orderService.GetOrders()
                                      .Count(order => order.CreatedDate.HasValue
                                                      && order.CreatedDate.Value.Date == today);
            TransactionCount = _transactionService.GetTransactions()
                                                  .Count(transaction => transaction.CreatedDate.HasValue
                                                                        && transaction.CreatedDate.Value.Date == today);
        }

        public IActionResult OnGet()
        {

            UserCount = _userService.GetUsers().Count;
            ProductCount = _productService.GetProducts().Count;
            CalculateAmountDaily();
            CalculateRevenueByDate(7);
            CalculateOrderStatusCounts();
            TotalOrderAndTransactionByDaily();
            RevenueFormatted = $"{Revenue.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} VND";          
            return Page();
        }
    }
}