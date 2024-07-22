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
        Task<Product> AddProduct(Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment);
        public Product GetProductById(string productId);
        Task<Product> UpdateProduct(string productId, Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment);
        public void DeleteProduct(string productId);
    }
}