using BOs.Entities;

using DAOs;

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
        public ProductService()
        {
            if (productRepo == null)
            {
                productRepo = new ProductRepository();
            }
        }
        public List<Product> GetProducts() => productRepo.GetProducts();
        public async Task AddProduct(Product product)
        {
            await productRepo.AddProduct(product);
        }
        public Product GetProductById(string productId) => productRepo.GetProductById(productId);
        public async Task UpdateProduct(string productId, Product product)
        {
            await productRepo.UpdateProduct(productId, product);
        }
        public void DeleteProduct(string productId) => productRepo.DeleteProduct(productId);

        public async Task UpdateProductQuantities(string productId, int quantities)
        {
            await productRepo.UpdateProductQuantities(productId, quantities);
        }

        public List<Product> GetProductsByCate(string cateId)
        {
            return productRepo.GetProductsByCate(cateId);
        }

        public List<Product> GetProductsBySearch(string searchQuery)
        {
            return productRepo.GetProductsBySearch(searchQuery);
        }

        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            return productRepo.GetProductsByPriceRange(minPrice, maxPrice);
        }
    }
}