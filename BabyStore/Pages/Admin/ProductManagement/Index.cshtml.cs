using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using System.Linq;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class IndexModel : PageModel
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public IndexModel()
        {
            productService = new ProductService();
            categoryService = new CategoryService();
        }

        public IList<Product> Product { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int count { get; set; }
        public int totalPages => (int)Math.Ceiling(Decimal.Divide(count, pageSize));

        [BindProperty(SupportsGet = true)]
        public string cateid { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }
        [BindProperty(SupportsGet = true)]
        public string name { get; set; }

        public IActionResult OnGet(int? pageIndex, string? cateid)
        {
            //if (HttpContext.Session.GetString("account") is null)
            //{
            //    return RedirectToPage("/Login");
            //}

            //var role = HttpContext.Session.GetString("account");

            //if (role != "admin")
            //{
            //    return RedirectToPage("/Login");
            //}

            Categories = categoryService.GetCategories().ToList();

            if (SearchText != null)
            {
                count = productService.GetProducts()
                    .Where(a => a.ProductId.ToUpper().Contains(SearchText.Trim().ToUpper()))
                    .Count();
                Product = productService.GetProducts()
            .Where(a => a.ProductId.Equals(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            .Skip((currentPage - 1) * pageSize).Take(pageSize)
            .ToList();
            }
            else
            {
                count = productService.GetProducts().Count();
                Product = productService.GetProducts()
                        .Skip((currentPage - 1) * pageSize).Take(pageSize)
                        .ToList();
            }
            if (cateid != null)
            {
                count = productService.GetProducts()
                    .Where(x => x.CateId.ToUpper().Contains(cateid.Trim().ToUpper()))
                    .Count();
                Product = productService.GetProducts()
            .Where(x => x.CateId.Equals(cateid, StringComparison.OrdinalIgnoreCase))
            .Skip((currentPage - 1) * pageSize).Take(pageSize)
            .ToList();
            }
            else
            {
                count = productService.GetProducts().Count();
                Product = productService.GetProducts()
                        .Skip((currentPage - 1) * pageSize).Take(pageSize)
                        .ToList();
            }
            if (name != null)
            {
                count = productService.GetProducts()
                    .Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()))
                    .Count();
                Product = productService.GetProducts()
            .Where(x => x.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
            .Skip((currentPage - 1) * pageSize).Take(pageSize)
            .ToList();
            }
            else
            {
                count = productService.GetProducts().Count();
                Product = productService.GetProducts()
                        .Skip((currentPage - 1) * pageSize).Take(pageSize)
                        .ToList();
            }
            return Page();
        }
    }
}
