using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.Utilities;

namespace BabyStore.Pages.Payment
{
    public class CallbackModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;

        public CallbackModel(ITransactionService transactionService, IOrderService orderService)
        {
            _transactionService = transactionService;
            _orderService = orderService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            VnPayLibrary vnpay = new VnPayLibrary();
            foreach (var param in Request.Query)
            {
                vnpay.AddResponseData(param.Key, param.Value);
            }

            string transactionId = vnpay.GetResponseData("vnp_TxnRef");
            string responseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string secureHash = Request.Query["vnp_SecureHash"];
            string orderId = vnpay.GetResponseData("vnp_OrderInfo").Split(':')[1].Trim();

            bool isValidSignature = vnpay.ValidateSignature(secureHash, "YOUR_HASHSECRET");

            if (isValidSignature)
            {
                if (responseCode == "00")
                {
                    await _transactionService.UpdateTransactionStatus(transactionId, 1); // success
                    await _orderService.UpdateOrderStatus(orderId, 1); // success
                    // Redirect to success page
                    return RedirectToPage("/Payment/Success");
                }
                else
                {
                    await _transactionService.UpdateTransactionStatus(transactionId, 3); // failed
                    await _orderService.UpdateOrderStatus(orderId, 3); // failed
                    // Redirect to failure page
                    return RedirectToPage("/Payment/Failure");
                }
            }
            else
            {
                await _transactionService.UpdateTransactionStatus(transactionId, 3); // failed
                await _orderService.UpdateOrderStatus(orderId, 3); // failed
                // Redirect to failure page
                return RedirectToPage("/Payment/Failure");
            }
        }
    }
}