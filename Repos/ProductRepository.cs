using BOs.Entities;
using DAOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO = null;
        public ProductRepository()
        {
            if (_productDAO == null)
            {
                _productDAO = new ProductDAO();
            }
        }
        public async Task AddProduct(Product product)
        {
             await _productDAO.AddProduct(product);
        }



        public void DeleteProduct(string productId) => _productDAO.DeleteProduct(productId);



        public Product GetProductById(string productId) => _productDAO.GetProductById(productId);



        public List<Product> GetProducts() => _productDAO.GetProducts();

        public List<Product> GetProductsByCate(string cateId)
        {
            return _productDAO.GetProductsByCate(cateId);
        }

        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            return _productDAO.GetProductsByPriceRange(minPrice, maxPrice);
        }

        public List<Product> GetProductsBySearch(string searchQuery)
        {
            return _productDAO.GetProductsBySearch(searchQuery);
        }

        public async Task UpdateProduct(string productId, Product product)
        {
            await _productDAO.UpdateProduct(productId , product);
        }

        public async Task UpdateProductQuantities(string productId, int quantities)
        {
            await _productDAO.UpdateProductQuantities(productId, quantities);
        }
    }
}