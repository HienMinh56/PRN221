using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using Services.Utilities;
using System.Web;

namespace BabyStore.Pages.Payment
{
    public class CallbackModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public CallbackModel(ITransactionService transactionService, IOrderService orderService, IPaymentService paymentService)
        {
            _transactionService = transactionService;
            _orderService = orderService;
            _paymentService = paymentService;
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


            string orderInfoEncoded = vnpay.GetResponseData("vnp_OrderInfo");
            string orderInfo = HttpUtility.UrlDecode(orderInfoEncoded);
            string orderId = orderInfo.Split(':')[1].Trim();

            bool isValidSignature = vnpay.ValidateSignature(secureHash, "IOACKGOPU0DSSB2UBRMFPVJ642X92RHQ");

            if (isValidSignature)
            {
                if (responseCode == "00")
                {
                    await _transactionService.UpdateTransactionStatus(transactionId, 1);
                    await _orderService.UpdateOrderStatus(orderId, 1);
                    await _paymentService.HandleSuccessfulPayment(orderId);
                    HttpContext.Session.Remove("CartItems");
                    return RedirectToPage("/Payment/Success", new { orderId = orderId });

                }
                else
                {
                    await _transactionService.UpdateTransactionStatus(transactionId, 3); 
                    await _orderService.UpdateOrderStatus(orderId, 3); 

                    HttpContext.Session.Remove("CartItems");
                    return RedirectToPage("/Payment/Failure");
                }
            }
            else
            {
                await _transactionService.UpdateTransactionStatus(transactionId, 3); 
                await _orderService.UpdateOrderStatus(orderId, 3); 

                return RedirectToPage("/Payment/Failure");
            }
        }
    }
}
