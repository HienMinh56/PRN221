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

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _product;

        public DeleteModel(IProductService product)
        {
            _product = product;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }

            var product = _product.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id, Product product)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }
            try
            {
                if (product != null)
                {
                    product.Status = 0;
                    await _product.UpdateProduct(id, product);
                }

                return RedirectToPage("./Product", new
                {
                    message = "Delete Successfull",
                    messageType = "success"
                });
            }
            catch (Exception ex)
            {
                return RedirectToPage("./Delete", new
                {
                    message = "Delete Failed",
                    messageType = "failed"
                });
            }
        }
    }
}
