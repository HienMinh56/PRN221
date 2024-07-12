using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                product = _dbprn221Context.Products
                            .AsNoTracking()
                            .Include(c => c.Cate)
                            .Include(c => c.Orders)
                            .Include(c => c.OrderDetails)
                            .FirstOrDefault(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public Product AddProduct(Product product)
        {
            try
            {
                var lastProduct = _dbprn221Context.Products
            .OrderByDescending(p => p.ProductId)
            .FirstOrDefault();

                if (lastProduct != null)
                {
                    // Assuming ProductId is in the format "product001", extract the numeric part
                    string lastProductId = lastProduct.ProductId;
                    string numericPart = lastProductId.Substring(7); // Adjust based on your format

                    if (int.TryParse(numericPart, out int number))
                    {
                        // Increment the number part
                        number++;
                        string newProductId = "PRODUCT" + number.ToString().PadLeft(3, '0'); // Format back to "product001"
                        product.ProductId = newProductId;
                    }
                    else
                    {
                        throw new Exception("Invalid ProductId format in the database.");
                    }
                }
                else
                {
                    // If there are no existing products, start with product001
                    product.ProductId = "PRODUCT001";
                }

                // Set other default values if necessary
                product.Quantity = 0;
                product.Status = 1;

                // Add the product to the context and save changes
                _dbprn221Context.Products.Add(product);
                _dbprn221Context.SaveChanges();
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

        public void UpdateProduct(string productId, Product product)
        {
            Product p = GetProductById(productId);
            if (p != null)
            {
                p.Name = product.Name;
                p.CateId = product.CateId;
                p.Price = product.Price;
                p.Title = product.Title;
                p.Description = product.Description;
                p.Image = product.Image;
                p.Quantity = product.Quantity;
                p.Status = product.Status;

                _dbprn221Context.Update(p);
                _dbprn221Context.SaveChanges();

            }
        }
    }
}
