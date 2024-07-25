using Microsoft.AspNetCore.Http;
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

            
            var allProducts = _productService.GetProducts();

            
            if (!string.IsNullOrEmpty(CateId))
            {
                allProducts = allProducts.Where(p => p.CateId == CateId).ToList();
            }

            
            if (MinPrice.HasValue && MaxPrice.HasValue)
            {
                allProducts = allProducts.Where(p => p.Price >= MinPrice.Value && p.Price <= MaxPrice.Value).ToList();
            }

            
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                allProducts = allProducts.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Product = allProducts;
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
                TempData["message"] = "Please Login to add product to cart";
                TempData["messageType"] = "error";
                return RedirectToPage("/UserMenu/AllProduct");
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
                return RedirectToPage("/UserMenu/AllProduct");
            }
            catch
            {
                TempData["message"] = "Add product to cart Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("/UserMenu/AllProduct");
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
