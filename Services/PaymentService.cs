using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BOs.Entities;
using Services.Interfaces;
using Services.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using BOs.Model.CartModel;
using System.Net;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;
        private readonly IUrlHelper _urlHelper;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IVoucherService _voucherService;
        private readonly IProductService _productService;

        public PaymentService(IHttpContextAccessor httpContextAccessor, ITransactionService transactionService, IOrderService orderService
                              , IUrlHelper urlHelper, IOrderDetailService orderDetailService, IVoucherService voucherService, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
            _orderService = orderService;
            _urlHelper = urlHelper;
            _orderDetailService = orderDetailService;
            _voucherService = voucherService;
            _productService = productService;
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

            VnPayLibrary vnPay = new VnPayLibrary();

            vnPay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPay.AddRequestData("vnp_Amount", (amount * 100).ToString()); 
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); 
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString());
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", WebUtility.UrlEncode("Payment for order: " + orderId)); 
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnPay.AddRequestData("vnp_TxnRef", transaction.TransactionId.ToString());
            vnPay.AddRequestData("vnp_BankCode", "NCB");

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

        public async Task<string> Checkout(string userId, int totalAmount, List<CartItem> cartItems, string voucherCode = null)
        {
            var orderId = await _orderService.CreateOrder(userId, totalAmount, cartItems, voucherCode);
            return await CreatePaymentUrl(userId, totalAmount, orderId);
        }

        public async Task HandleSuccessfulPayment(string orderId)
        {
            var orderDetails = _orderDetailService.GetOrderDetails(orderId);
            foreach (var orderDetail in orderDetails)
            {
                await _productService.UpdateProductQuantities(orderDetail.ProductId, orderDetail.Quantity);
            }

            var order =  _orderService.GetOrderById(orderId);
            if (!string.IsNullOrEmpty(order.VoucherCode))
            {
                var voucher =  _voucherService.GetVoucherByCode(order.VoucherCode);
                if (voucher != null)
                {
                    voucher.Quantity--;
                    await _voucherService.UpdateVoucher(voucher);
                }
            }
        }
    }
}
