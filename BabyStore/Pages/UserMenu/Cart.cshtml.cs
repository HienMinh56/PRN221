using BabyStore.Extensions;
using BOs.Entities;
using BOs.Model.CartModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyStore.Pages.UserMenu
{
    public class CartModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<CartModel> _logger;

        public CartModel(IVoucherService voucherService, IPaymentService paymentService, ILogger<CartModel> logger)
        {
            _voucherService = voucherService;
            _paymentService = paymentService;
            _logger = logger;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public double TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public IList<Voucher> ActiveVouchers { get; set; } = new List<Voucher>();
        public string AppliedVoucherCode { get; set; } = string.Empty;
        public double FinalPrice { get; set; }

        public void OnGet()
        {
            LoadCart();
            LoadActiveVouchers();
            CalculateFinalPrice();
        }

        private void LoadCart()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            TotalPrice = CartItems.Sum(item => item.Quantity * item.Price);
            ShippingAddress = HttpContext.Session.GetString("address") ?? "Pls Update your address.";
        }

        private void LoadActiveVouchers()
        {
            ActiveVouchers = _voucherService.GetVouchersActive().ToList();
        }

        private double CalculateFinalPrice()
        {
            if (!string.IsNullOrEmpty(AppliedVoucherCode))
            {
                var voucher = ActiveVouchers.FirstOrDefault(v => v.VoucherCode == AppliedVoucherCode);
                if (voucher != null)
                {
                    var discountAmount = (TotalPrice * voucher.Discount / 100);
                    FinalPrice = TotalPrice - discountAmount;
                    FinalPrice = Math.Max(FinalPrice, 0);
                    return FinalPrice;
                }
                else
                {
                    FinalPrice = TotalPrice;
                    return FinalPrice;
                }
            }
            else
            {
                FinalPrice = TotalPrice;
                return FinalPrice;
            }
        }

        public IActionResult OnPostRemoveItem(string productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                _logger.LogInformation($"Removed item {productId} from cart.");
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUpdateQuantity(string productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                    _logger.LogInformation($"Updated quantity for item {productId} to {quantity}.");
                }
                else
                {
                    cart.Remove(cartItem);
                    _logger.LogInformation($"Removed item {productId} from cart due to quantity zero.");
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);

                LoadCart();
                CalculateFinalPrice();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostApplyVoucher(string voucherCode)
        {
            LoadCart();
            LoadActiveVouchers();

            var voucher = ActiveVouchers.FirstOrDefault(v => v.VoucherCode == voucherCode);
            if (voucher == null)
            {
                ModelState.AddModelError(string.Empty, "Voucher không hợp lệ.");
                _logger.LogWarning($"Attempted to apply invalid voucher code: {voucherCode}.");
                return Page();
            }

            if (TotalPrice < voucher.MinimumOrderAmount)
            {
                ModelState.AddModelError(string.Empty, $"Tổng tiền hiện tại không đủ để áp dụng voucher. Cần thêm {voucher.MinimumOrderAmount - TotalPrice}₫.");
                _logger.LogWarning($"Total price {TotalPrice} is less than minimum order amount {voucher.MinimumOrderAmount} for voucher code: {voucherCode}.");
                return Page();
            }

            AppliedVoucherCode = voucherCode;
            CalculateFinalPrice();
            _logger.LogInformation($"Applied voucher code: {voucherCode}.");

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckout(double FinalPrice, string AppliedVoucherCode)
        {
            LoadCart();
            LoadActiveVouchers(); // Ensure vouchers are loaded

            string userId = HttpContext.Session.GetString("id");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is missing during checkout, redirecting to login.");
                return RedirectToPage("/Login", new { returnUrl = "/UserMenu/Cart" });
            }

            

            string paymentUrl = await _paymentService.Checkout(userId, (int)(decimal)FinalPrice, CartItems, AppliedVoucherCode);

            HttpContext.Session.Remove("Cart");
            _logger.LogInformation($"Checkout initiated for user {userId}. Cart cleared. Redirecting to payment URL.");

            return Redirect(paymentUrl);
        }


    }
}