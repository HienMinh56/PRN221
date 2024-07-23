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

        public CartModel(IVoucherService voucherService, IPaymentService paymentService)
        {
            _voucherService = voucherService;
            _paymentService = paymentService;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public double TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public IList<Voucher> ActiveVouchers { get; set; } = new List<Voucher>();
        public string AppliedVoucherCode { get; set; } = string.Empty;
        public double FinalPrice { get; set; }

        public void OnGet()
        {
            // Get cart items and total price
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            TotalPrice = CartItems.Sum(item => item.Quantity * item.Price);
            ShippingAddress = HttpContext.Session.GetString("address") ?? "Pls Update your address.";

            // Get active vouchers
            ActiveVouchers = _voucherService.GetVouchersActive().ToList();

            // Calculate final price after applying voucher
            if (!string.IsNullOrEmpty(AppliedVoucherCode))
            {
                var voucher = ActiveVouchers.FirstOrDefault(v => v.VoucherCode == AppliedVoucherCode);
                if (voucher != null)
                {
                    var discountAmount = (TotalPrice * voucher.Discount / 100);
                    FinalPrice = TotalPrice - discountAmount;
                    FinalPrice = Math.Max(FinalPrice, 0);
                }
                else
                {
                    FinalPrice = TotalPrice;
                }
            }
            else
            {
                FinalPrice = TotalPrice;
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
                }
                else
                {
                    cart.Remove(cartItem);
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);

                // Update total price
                TotalPrice = cart.Sum(item => item.Quantity * item.Price);

                // Recalculate final price after applying voucher
                if (!string.IsNullOrEmpty(AppliedVoucherCode))
                {
                    var voucher = ActiveVouchers.FirstOrDefault(v => v.VoucherCode == AppliedVoucherCode);
                    if (voucher != null)
                    {
                        var discountAmount = voucher.Discount;
                        FinalPrice = TotalPrice - (TotalPrice * discountAmount);
                        FinalPrice = Math.Max(FinalPrice, 0);
                    }
                    else
                    {
                        FinalPrice = TotalPrice;
                    }
                }
                else
                {
                    FinalPrice = TotalPrice;
                }
            }

            return RedirectToPage();
        }

        public IActionResult OnPostApplyVoucher(string voucherCode)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            TotalPrice = cart.Sum(item => item.Quantity * item.Price);

            var voucher = ActiveVouchers.FirstOrDefault(v => v.VoucherCode == voucherCode);
            if (voucher == null)
            {
                ModelState.AddModelError(string.Empty, "Voucher không hợp lệ.");
                return Page();
            }

            if (TotalPrice < voucher.MinimumOrderAmount)
            {
                ModelState.AddModelError(string.Empty, $"Tổng tiền hiện tại không đủ để áp dụng voucher. Cần thêm {voucher.MinimumOrderAmount - TotalPrice}₫.");
                return Page();
            }

            AppliedVoucherCode = voucherCode;

            // Calculate discount and final price
            var discountAmount = (TotalPrice * voucher.Discount / 100);
            FinalPrice = TotalPrice - discountAmount;
            FinalPrice = Math.Max(FinalPrice, 0);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckout()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            TotalPrice = CartItems.Sum(item => item.Quantity * item.Price);

            string userId = HttpContext.Session.GetString("id");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login", new { returnUrl = "/UserMenu/Cart" });
            }

            string paymentUrl = await _paymentService.Checkout(userId, (int)(decimal)TotalPrice, CartItems);

            return Redirect(paymentUrl); 
        }
    }
}