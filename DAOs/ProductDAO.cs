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
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };
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
        
        public async Task<Product> AddProduct(Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment)
        {
            try
            {
                product.ProductId = GenerateNewProductId();
                product.Status = 1;

                if (image != null)
                {
                    product.Image = await AddImage(image, enviroment);
                }
                // Add the product to the context and save changes
                _dbprn221Context.Products.Add(product);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
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

        public async Task<Product> UpdateProduct(string productId, Product product, IFormFile image, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment)
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

                // Update the image if a new one is provided
                if (image != null)
                {
                    p.Image = await AddImage(image, enviroment);
                }

                _dbprn221Context.Update(p);
                await _dbprn221Context.SaveChangesAsync();
            }
            return p;
        }

        public string GenerateNewProductId()
        {
            string newProductId = "PRODUCT001"; // Default starting value
            // Find the last product in the database to determine the next number
            var lastProduct = _dbprn221Context.Products
                .OrderByDescending(p => p.ProductId)
                .FirstOrDefault();

            if (lastProduct != null)
            {
                // Assuming ProductId is in the format "PRODUCT001", extract the numeric part
                string lastProductId = lastProduct.ProductId;
                string numericPart = lastProductId.Substring(7); // Adjust based on your format

                if (int.TryParse(numericPart, out int number))
                {
                    // Increment the number part
                    number++;
                    newProductId = "PRODUCT" + number.ToString().PadLeft(3, '0'); // Format back to "PRODUCT001"
                }
                else
                {
                    throw new Exception("Invalid ProductId format in the database.");
                }
            }

            return newProductId;
        }

        public async Task<string?> AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment enviroment)
        {
            if (file != null)
            {
                foreach (var f in ImageExtensions)
                {
                    if (file.FileName.Contains(f))
                    {
                        var fileUp = Path.Combine(enviroment.WebRootPath, "images", file.FileName);
                        using (var fileStream = new FileStream(fileUp, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            return $"/images/{file.FileName}";
                        }
                    }
                }
            }
            return null;
        }
    }
}