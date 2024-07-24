using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.Globalization;
using System.Threading.Tasks;

namespace BabyStore.Pages.Payment
{
    public class SuccessModel : PageModel
    {
        private readonly IEmailService _emailService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public SuccessModel(IEmailService emailService, IOrderService orderService, IUserService userService, ITransactionService transactionService)
        {
            _emailService = emailService;
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            var user = _userService.GetUserById(order.UserId);
            var userEmail = user.Email;

            var totalAmount = order.TotalAmount.ToString("N0", new CultureInfo("vi-VN")) + " VND";
            var subject = "Payment Successful";
            var body = $@"
            <html>
            <body style=""font-family: Arial, sans-serif; line-height: 1.6;"">
                <div style=""max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;"">
                    <h2 style=""text-align: center; color: #4CAF50;"">Payment Successful</h2>
                    <p>Dear <strong>{user.FullName}</strong>,</p>
                    <p>Your order has been paid successfully!</p>
                    <table style=""width: 100%; border-collapse: collapse;"">
                        <tr>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">OrderId:</td>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">{orderId}</td>
                        </tr>
                        <tr>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">Voucher:</td>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">{order.VoucherCode}</td>
                        </tr>
                        <tr>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">Total Amount:</td>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">{totalAmount}</td>
                        </tr>
                        <tr>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">Date of purchase:</td>
                            <td style=""padding: 8px; border: 1px solid #ddd;"">{order.CreatedDate}</td>
                        </tr>    
                    </table>
                     <p>Thank you for shopping with us.</p>
                </div>
            </body>
            </html>";
            await _emailService.SendEmailAsync(userEmail, subject, body);

            return Page();
        }
    }
}
