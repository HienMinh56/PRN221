using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BabyStore.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;

        public DashboardModel(IUserService userService,
                              ITransactionService transactionService,
                              IOrderService orderService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _orderService = orderService;
        }

        public int UserCount { get; private set; }
        public int TransactionCount { get; private set; }
        public int OrderCount { get; private set; }
        public int Account1 { get; private set; }
        public int Account0 { get; private set; }

        public IActionResult OnGet()
        {
            //if (HttpContext.Session.GetString("account") is null)
            //{
            //    return RedirectToPage("/Login");
            //}

            //var Role = HttpContext.Session.GetString("account");

            //if (Role != "1")
            //{
            //    return RedirectToPage("/Login");
            //}

            UserCount = _userService.GetUsers().Count;
            TransactionCount = _transactionService.GetTransactions().Count;
            OrderCount = _orderService.GetOrders().Count;
            Account1 = _userService.GetUsers().Where(s => s.Status == 1).ToList().Count();
            Account0 = _userService.GetUsers().Where(s => s.Status == 0).ToList().Count();

            return Page();
        }
    }
}