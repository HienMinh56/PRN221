using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class DeleteModel : PageModel
    {
        private readonly BOs.Dbprn221Context _context;

        public DeleteModel(BOs.Dbprn221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Cate).FirstOrDefaultAsync(m => m.ProductId.Equals(id));

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

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product != null)
            {
                product.Status = 0; // Corrected order
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
