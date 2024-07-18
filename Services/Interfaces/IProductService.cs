using BabyStore.Helper;
using BOs.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Product AddProduct(Product product);
        public Product GetProductById(string productId);
        Task<Product> UpdateProduct(string productId, Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment);
        public void DeleteProduct(string productId);
        public Task<PaginatedList<Product>> GetProductsByCategoryAsync(string category, int pageIndex, int pageSize);
    }
}