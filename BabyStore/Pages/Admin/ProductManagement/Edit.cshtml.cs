using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;
using Services;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class EditModel : PageModel
    {
        private readonly BOs.Dbprn221Context _context;
        private readonly IImageHandle _imageHandle;
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public EditModel(BOs.Dbprn221Context context, IImageHandle imageHandle, ICategoryService category, IProductService product, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _imageHandle = imageHandle;
            _environment = environment;
            _product = product;
            _category = category;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;
        [Required(ErrorMessage = "Choose one File!")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,jpe,bmp,gif,png")]
        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _product.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            ViewData["CateId"] = new SelectList(_category.GetCategories(), "CateId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id, Product product)
        {
            try
            {
                Product = await _product.UpdateProduct(id, product, Image, _environment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Product");
        }

        private bool ProductExists(string? id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
