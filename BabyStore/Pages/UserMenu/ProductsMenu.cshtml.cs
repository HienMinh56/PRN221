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
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; } = string.Empty;
        public IList<Product> Product { get;set; } = default!;
        [BindProperty]
        public string ProductId { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public string ProductImage { get; set; }

        public async Task OnGetAsync()
        {
            var allProducts = _productService.GetProducts().Where(p=>p.Status==1);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Product = allProducts.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                Product = allProducts.ToList();
            };
        }
        public bool IsProductInCart(string productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return cart.Any(item => item.ProductId == productId);
        }
        public IActionResult OnPostAddToCart(string productId, string productName, int price, string productImage, int availableQuantity)
        {
            var isAuthenticated = !string.IsNullOrEmpty(HttpContext.Session.GetString("username"));
            if (!isAuthenticated)
            {
                TempData["message"] = "Please login to add product to cart";
                TempData["messageType"] = "error";
                return RedirectToPage("/UserMenu/ProductsMenu");
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
                        AvailableQuantity = availableQuantity
                    });
                }
                else
                {
                    cartItem.Quantity++;
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);

                TempData["message"] = "Add product to cart Successful";
                TempData["messageType"] = "success";
                return RedirectToPage("/UserMenu/ProductsMenu");
            }
            catch
            {
                TempData["message"] = "Add product to cart Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("/UserMenu/ProductsMenu");
            }
        }


    }
}
