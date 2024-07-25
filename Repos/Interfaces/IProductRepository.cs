using BOs.Entities;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        public void DeleteProduct(string productId);
        public Product GetProductById(string productId);
        public List<Product> GetProducts();
        Task UpdateProduct(string productId, Product product);
        Task UpdateProductQuantities(string productId, int quantities);
        public List<Product> GetProductsByCate(string cateId);
        public List<Product> GetProductsBySearch(string searchQuery);
        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice);
        public Task<bool> ProductExists(string name, string productId = null);
    }
}