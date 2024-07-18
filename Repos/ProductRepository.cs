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
        public Product AddProduct(Product product) => _productDAO.AddProduct(product);



        public void DeleteProduct(string productId) => _productDAO.DeleteProduct(productId);



        public Product GetProductById(string productId) => _productDAO.GetProductById(productId);



        public List<Product> GetProducts() => _productDAO.GetProducts();

       

        public async Task<Product> UpdateProduct(string productId, Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment)
        {
            return await _productDAO.UpdateProduct(productId, product, image, enviroment);
        }
    }
}