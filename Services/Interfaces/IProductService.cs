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
        Task AddProduct(Product product);
        public Product GetProductById(string productId);
        Task UpdateProduct(string productId, Product product);
        Task UpdateProductQuantities(string productId, int quantities);
        public void DeleteProduct(string productId);
        public List<Product> GetProductsByCate(string cateId);
        public List<Product> GetProductsBySearch(string searchQuery);
        public List<Product> GetProductsByPriceRange(int? minPrice, int? maxPrice);
    }
}