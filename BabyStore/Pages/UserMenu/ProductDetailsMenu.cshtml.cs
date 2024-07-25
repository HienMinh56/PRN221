using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services;
using BabyStore.Extensions;
using BOs.Model.CartModel;

namespace BabyStore.Pages.UserMenu
{
    public class ProductDetailsMenuModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductDetailsMenuModel(IProductService productService)
        {
            _productService = productService;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }
        public bool IsProductInCart(string productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return cart.Any(item => item.ProductId == productId);
        }
        public IActionResult OnPostAddToCart(string productId, string productName, int price, string productImage, int availableQunatity)
        {
            var isAuthenticated = !string.IsNullOrEmpty(HttpContext.Session.GetString("username"));
            if (!isAuthenticated)
            {
                return RedirectToPage("/UserMenu/ProductDetailsMenu", new { id = productId, message = "Please log in to add items to your cart", messageType = "error" });
            }

            try
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (cartItem == null)
                {
                    cart.Add(new CartItem
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Price = price,
                        ProductImage = productImage,
                        Quantity = 1,
                        AvailableQuantity = availableQunatity
                    });
                }
                else
                {
                    cartItem.Quantity++;
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);
                TempData["message"] = "Add product to cart Successful";
                TempData["messageType"] = "success";
                return RedirectToPage("/UserMenu/ProductDetailsMenu");
            }
            catch
            {
                TempData["message"] = "Add product to cart Failed";
                TempData["messageType"] = "success";
                return RedirectToPage("/UserMenu/ProductDetailsMenu");
            }
        }


    }
}
