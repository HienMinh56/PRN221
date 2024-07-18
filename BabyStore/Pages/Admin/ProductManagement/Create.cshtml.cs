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
            Product.ProductId = GenerateNewProductId();
            Product.Status = 1;
            Product.Image = await _imageHandle.AddImage(Image, _environment);
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public string GenerateNewProductId()
        {
            string newProductId = "PRODUCT001"; // Default starting value
            // Find the last product in the database to determine the next number
            var lastProduct = _context.Products
                .OrderByDescending(p => p.ProductId)
                .FirstOrDefault();

            if (lastProduct != null)
            {
                // Assuming ProductId is in the format "PRODUCT001", extract the numeric part
                string lastProductId = lastProduct.ProductId;
                string numericPart = lastProductId.Substring(7); // Adjust based on your format

                if (int.TryParse(numericPart, out int number))
                {
                    // Increment the number part
                    number++;
                    newProductId = "PRODUCT" + number.ToString().PadLeft(3, '0'); // Format back to "PRODUCT001"
                }
                else
                {
                    throw new Exception("Invalid ProductId format in the database.");
                }
            }

            return newProductId;
        }
        //public string UploadFile()
        //{
        //    string uniqueFileName = null;
        //    if (FrontImage != null)
        //    {
        //        string fileUp = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + FrontImage.FileName;
        //        string filePath = Path.Combine(fileUp, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            FrontImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}