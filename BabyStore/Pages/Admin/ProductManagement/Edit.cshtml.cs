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
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly ICloudStorageService _cloudStorageService;

        public EditModel(ICloudStorageService cloudStorageService, ICategoryService category, IProductService product)
        {
            _cloudStorageService = cloudStorageService;
            _product = product;
            _category = category;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

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
            if (Image != null)
            {
                var fileName = $"images/{Image.FileName}";
                product.Image = await _cloudStorageService.UploadFileAsync(Image, fileName, 500, 500);
            }
            await _product.UpdateProduct(id, product);

            return RedirectToPage("./Product", new
            {
                message = "Updated Successfull",
                messageType = "success"
            });
        }
    }
}
