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
                listProduct = _dbprn221Context.Products
                                              .OrderByDescending(p => p.ProductId)
                                              .ToList();
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
                product = _dbprn221Context.Products.Where(p => p.ProductId == productId)
                                          .OrderByDescending(p => p.ProductId)
                                          .FirstOrDefault();
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
                var lastProduct = await _dbprn221Context.Products
                    .OrderByDescending(p => p.ProductId)
                    .FirstOrDefaultAsync();

                if (lastProduct != null)
                {
                    string lastProductId = lastProduct.ProductId;
                    int lastNumber = int.Parse(lastProductId.Substring(7));
                    int newNumber = lastNumber + 1;
                    product.ProductId = $"PRODUCT{newNumber:D3}";
                }
                else
                {
                    product.ProductId = "PRODUCT001";
                }

                _dbprn221Context.Products.Add(product);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ProductExists(string name, string excludeProductId = null)
        {
            var product = await _dbprn221Context.Products
                .Where(p => (p.Name == name) && p.ProductId != excludeProductId)
                .FirstOrDefaultAsync();

            return product != null;
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

                // Kiểm tra xem product.Image có giá trị hay không
                if (product.Image != null)
                {
                    p.Image = product.Image;
                }

                p.Status = product.Status;

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
        public List<Product> GetProductsByPriceRange(int? minPrice, int? maxPrice)
        {
            var query = _dbprn221Context.Products.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return query.ToList();
        }
    }
}

