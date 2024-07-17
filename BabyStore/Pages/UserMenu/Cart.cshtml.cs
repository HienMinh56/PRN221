using BabyStore.Extensions;
using BOs.Model.CartModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BabyStore.Pages.UserMenu
{
    public class CartModel : PageModel
    {

        public CartModel()
        {
        }

        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }
    }
}
