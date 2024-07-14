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

        public async Task OnGetAsync()
        {
            Product =  _productService.GetProducts();
        }
    }
}
