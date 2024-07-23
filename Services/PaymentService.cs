using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BOs.Entities;
using Services.Interfaces;
using Services.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using BOs.Model.CartModel;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;
        private readonly IUrlHelper _urlHelper;

        public PaymentService(IHttpContextAccessor httpContextAccessor, ITransactionService transactionService, IOrderService orderService, IUrlHelper urlHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
            _orderService = orderService;
            _urlHelper = urlHelper;
        }

        public async Task<string> CreatePaymentUrl(string userId, int amount, string orderId)
        {
            var transactionId = await _transactionService.GenerateTransactionId();
            var transaction = new Transaction
            {
                TransactionId = transactionId,
                UserId = userId,
                Amount = amount,
                Status = 2, // Pending
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                OrderId = orderId
            };
            await _transactionService.AddTransaction(transaction);

            string vnp_Returnurl = _urlHelper.Page("/Payment/Callback", null, new { area = "" }, _httpContextAccessor.HttpContext.Request.Scheme);
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = "CA7EZUZY";
            string vnp_HashSecret = "IOACKGOPU0DSSB2UBRMFPVJ642X92RHQ";

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.0.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)(amount * 100)).ToString());
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", transaction.TransactionId);
            vnpay.AddRequestData("vnp_OrderInfo", "Payment for order: " + orderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_IpAddr", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

        public async Task<string> Checkout(string userId, int totalAmount, List<CartItem> cartItems)
        {
            var orderId = await _orderService.CreateOrder(userId, totalAmount, cartItems);
            return await CreatePaymentUrl(userId, totalAmount, orderId);
        }
    }
}
