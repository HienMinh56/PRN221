using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;
using BOs.Entities;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class CreateModel : PageModel
    {
        private readonly BOs.Dbprn221Context _context;

        public CreateModel(BOs.Dbprn221Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CateId"] = new SelectList(_context.Categories, "CateId", "CateId");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
