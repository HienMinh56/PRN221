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
    public class DetailsModel : PageModel
    {
        private readonly BOs.Dbprn221Context _context;

        public DetailsModel(BOs.Dbprn221Context context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(m => m.Cate).FirstOrDefaultAsync(m => m.ProductId.Equals(id));
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
    }
}
