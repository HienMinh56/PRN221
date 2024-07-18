using BabyStore.Extensions;
using BOs.Model.CartModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BabyStore.Pages.UserMenu
{
    public class CartModel : PageModel
    {

        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            TotalPrice = CartItems.Sum(item => item.Quantity * item.Price);
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
    }
}
