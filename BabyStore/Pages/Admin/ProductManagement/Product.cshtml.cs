using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using System.Data;
using System.Linq;

namespace BabyStore.Pages.Admin.ProductManagement
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 8;

        public IndexModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IList<Product> Product { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? CateId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ProductId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ProductName { get; set; }

        public IActionResult OnGet(int? pageIndex, string? cateId, int? status, string? productId, string? productName)
        {
            Categories = _categoryService.GetCategories().ToList();

            CateId = cateId;
            Status = status;
            ProductId = productId;
            ProductName = productName;

            var ProductList = _productService.GetProducts();

            if (!string.IsNullOrEmpty(ProductId))
            {
                ProductList = ProductList.Where(a => a.ProductId.ToUpper().Contains(ProductId.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(ProductName))
            {
                ProductList = ProductList.Where(a => a.Name.ToUpper().Contains(ProductName.Trim().ToUpper())).ToList();
            }

            if (Status.HasValue)
            {
                ProductList = ProductList.Where(a => a.Status == Status.Value).ToList();
            }

            if (!string.IsNullOrEmpty(CateId))
            {
                ProductList = ProductList.Where(a => a.CateId.ToUpper().Contains(CateId.Trim().ToUpper())).ToList();
            }

            PageIndex = pageIndex ?? 1;

            // Paginate the filtered list
            var count = ProductList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = ProductList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Product = items;
            return Page();
        }

        public IActionResult OnPost(int? pageIndex)
        {
            return RedirectToPage(new
            {
                pageIndex,
                cateId = CateId,
                status = Status,
                productId = ProductId,
                productName = ProductName
            });
        }

    }
}