﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs.Entities;
using Services.Interfaces;
using BOs.Model.CartModel;
using BabyStore.Extensions;
using Services;

namespace BabyStore.Pages.UserMenu
{
    public class AllProductModel : PageModel
    {
        private readonly IProductService _productService;

        public AllProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public string CateId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public async Task OnGetAsync(bool clearFilters = false)
        {
            if (clearFilters)
            {
                CateId = null;
                MinPrice = null;
                MaxPrice = null;
                SearchQuery = null;
            }

            // Get all products
            var allProducts = _productService.GetProducts();

            // Apply category filter if specified
            if (!string.IsNullOrEmpty(CateId))
            {
                allProducts = allProducts.Where(p => p.CateId == CateId).ToList();
            }

            // Apply price range filter if specified
            if (MinPrice.HasValue && MaxPrice.HasValue)
            {
                allProducts = allProducts.Where(p => p.Price >= MinPrice.Value && p.Price <= MaxPrice.Value).ToList();
            }

            // Apply search query filter if specified
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                allProducts = allProducts.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Product = allProducts;
        }

        public IActionResult OnPostAddToCart(string productId, string productName, int price, string productImage, int availableQuantity)
        {
            var isAuthenticated = !string.IsNullOrEmpty(HttpContext.Session.GetString("username"));
            if (!isAuthenticated)
            {
                // Redirect to the AllProduct page with a login required message
                return RedirectToPage("/UserMenu/AllProduct", new
                {
                    message = "Please log in to add items to your cart",
                    messageType = "error"
                });
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

                // Redirect to the AllProduct page with a success message
                return RedirectToPage("/UserMenu/AllProduct", new
                {
                    message = "Add Successful",
                    messageType = "success"
                });
            }
            catch
            {
                // Redirect to the AllProduct page with an error message
                return RedirectToPage("/UserMenu/AllProduct", new
                {
                    message = "Add failed",
                    messageType = "error"
                });
            }
        }

        public IActionResult OnGetFilterByCategory(string cateId)
        {
            CateId = cateId;
            return RedirectToPage();
        }

        public IActionResult OnGetFilterByPrice(int minPrice, int maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            return RedirectToPage();
        }
    }

}