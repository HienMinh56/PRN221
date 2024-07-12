using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public interface IProductRepository
    {
        public Product AddProduct(Product product);
        public void DeleteProduct(string productId);
        public Product GetProductById(string productId);
        public List<Product> GetProducts();
        public void UpdateProduct(string productId, Product product);
    }
}
