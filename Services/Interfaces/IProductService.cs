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
        public void UpdateProduct(string productId, Product product);
        public void DeleteProduct(string productId);
    }
}