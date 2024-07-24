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
    }
}