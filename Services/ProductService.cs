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
        public Product AddProduct(Product product) => productRepo.AddProduct(product);
        public Product GetProductById(string productId) => productRepo.GetProductById(productId);
        public async Task<Product> UpdateProduct(string productId, Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment)
        {
            return await productRepo.UpdateProduct(productId, product, image, enviroment);
        }
        public void DeleteProduct(string productId) => productRepo.DeleteProduct(productId);

        
    }
}