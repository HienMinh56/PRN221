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
using Services.Interfaces;
using BabyStore.Extensions;
using BOs.Model.CartModel;
using Microsoft.CodeAnalysis;

namespace BabyStore.Pages.UserMenu
{
    public class ProductsMenuModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductsMenuModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get;set; } = default!;
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public string ProductImage { get; set; }

        public async Task OnGetAsync()
        {
            Product =  _productService.GetProducts();
        }

        public IActionResult OnPostAddToCart(int productId, string productName, int price, string productImage)
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
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToPage("/UserMenu/ProductsMenu");
        }

    }
}
