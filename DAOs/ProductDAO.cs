using BOs;
using BOs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using static System.Net.Mime.MediaTypeNames;


namespace DAOs
{
    public class ProductDAO
    {
        private readonly Dbprn221Context _dbprn221Context;
        private static ProductDAO instance = null;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }


        public ProductDAO()
        {
            _dbprn221Context = new Dbprn221Context();
        }

        public List<Product> GetProducts()
        {
            List<Product> listProduct = null;
            try
            {
                listProduct = _dbprn221Context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProduct;
        }

        public Product GetProductById(string productId)
        {
            Product product = null;
            try
            {
                product = _dbprn221Context.Products.FirstOrDefault(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                // Fetch the last product from the database
                var lastProduct = await _dbprn221Context.Products
                    .OrderByDescending(p => p.ProductId)
                    .FirstOrDefaultAsync();

                // Generate new ProductId
                if (lastProduct != null)
                {
                    string lastProductId = lastProduct.ProductId;
                    int lastNumber = int.Parse(lastProductId.Substring(7));
                    int newNumber = lastNumber + 1;
                    product.ProductId = $"PRODUCT{newNumber:D3}";
                }
                else
                {
                    product.ProductId = "PRODUCT001"; // Start with PRODUCT001 if there are no products
                }

                _dbprn221Context.Products.Add(product);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }
        }


        public void DeleteProduct(string productId)
        {
            Product p = GetProductById(productId);
            if (p != null)
            {
                _dbprn221Context.Products.Remove(p);
                _dbprn221Context.SaveChanges();
            }
        }

        public async Task UpdateProduct(string productId, Product product)
        {
            Product p = GetProductById(productId);
            if (p != null)
            {
                p.Name = product.Name;
                p.CateId = product.CateId;
                p.Price = product.Price;
                p.Title = product.Title;
                p.Description = product.Description;
                p.Quantity = product.Quantity;
                p.Image = product.Image;

                _dbprn221Context.Update(p);
                await _dbprn221Context.SaveChangesAsync();
            }
        }
        public async Task UpdateProductQuantities(string productId, int quantity)
        {
            Product p = GetProductById(productId);
            if (p != null)
            {
                p.Quantity -= quantity;
                _dbprn221Context.Update(p);
                await _dbprn221Context.SaveChangesAsync();
            }
        }
        public List<Product> GetProductsByCate(string cateId)
        {
            return _dbprn221Context.Products.Where(p => p.CateId == cateId).ToList();
        }
        public List<Product> GetProductsBySearch(string searchQuery)
        {
            return _dbprn221Context.Products.Where(p => p.Name.Contains(searchQuery)).ToList();
        }
        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            return _dbprn221Context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }
    }
}

