using BabyStore.Helper;
using BOs.Entities;
using DAOs;
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

        public Task<PaginatedList<Product>> GetProductsByCategoryAsync(string category, int pageIndex, int pageSize)
        {
            return _productDAO.GetProductsByCategoryAsync(category, pageIndex, pageSize);
        }

        public void UpdateProduct(string productId, Product product) => _productDAO.UpdateProduct(productId, product);
    }
}