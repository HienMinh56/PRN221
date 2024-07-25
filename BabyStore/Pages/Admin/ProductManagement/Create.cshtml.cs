using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;
using BOs.Entities;
using Services.Interfaces;
using Services;
using Microsoft.Identity.Client.Extensions.Msal;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly ICloudStorageService _cloudStorageService;

        public CreateModel(ICategoryService category, IProductService product, ICloudStorageService cloudStorageService)
        {
            _category = category;
            _product = product;
            _cloudStorageService = cloudStorageService;
        }

        public IActionResult OnGet()
        {
            ViewData["CateId"] = new SelectList(_category.GetCategories(), "CateId", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Image != null)
                {
                    var fileName = $"images/{Image.FileName}";
                    Product.Image = await _cloudStorageService.UploadFileAsync(Image, fileName, 100, 100);
                }

                var productadd =  HttpContext.Session.GetString("username");

                Product.CreatedBy = productadd;
                Product.CreatedDate = DateTime.Now;

                await _product.AddProduct(Product);
                TempData["message"] = "Create Product Successful";
                TempData["messageType"] = "success";
                return RedirectToPage("./Product");
            }
            catch (Exception ex)
            {
                TempData["message"] = "Create Product Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("./Product");
            }
        }
    }
}