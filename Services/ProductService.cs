using BOs.Entities;
using Microsoft.AspNetCore.Http;
using Repos;
using Repos.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepo = null;
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };
        public ProductService()
        {
            if (productRepo == null)
            {
                productRepo = new ProductRepository();
            }
        }
        public List<Product> GetProducts() => productRepo.GetProducts();
        public Product AddProduct(Product product) => productRepo.AddProduct(product);
        public Product GetProductById(string productId) => productRepo.GetProductById(productId);
        public void UpdateProduct(string productId, Product product) => productRepo.UpdateProduct(productId, product);
        public void DeleteProduct(string productId) => productRepo.DeleteProduct(productId);
        public async Task<string?> AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            if (file != null)
            {
                foreach (var f in ImageExtensions)
                {
                    if (file.FileName.Contains(f))
                    {
                        var fileUp = Path.Combine(environment.WebRootPath, "images", file.FileName);
                        using (var fileStream = new FileStream(fileUp, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            return $"/images/{file.FileName}";
                        }
                    }
                }
            }
            return null;
        }
    }
}