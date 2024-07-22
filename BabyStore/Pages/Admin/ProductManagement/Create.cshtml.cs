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
        private readonly Dbprn221Context _context;
        private readonly IImageHandle _imageHandle;
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CreateModel(Dbprn221Context context, IImageHandle imageHandle, ICategoryService category, IProductService product, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _imageHandle = imageHandle;
            _category = category;
            _product = product;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            ViewData["CateId"] = new SelectList(_category.GetCategories(), "CateId", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!; 
        [Required(ErrorMessage = "Choose one File!")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,jpe,bmp,gif,png")]
        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //try
            //{
            //    Product = await _product.AddProduct(Product, Image, _environment); // Use the bound Product property
            //    return RedirectToPage("./Product");
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message); // Handle and display error
            return Page();
            //}
        }
    }
}