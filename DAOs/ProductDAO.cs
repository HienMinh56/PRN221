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
                // Add the product to the context and save changes
                _dbprn221Context.Products.Add(product);
                _dbprn221Context.SaveChangesAsync();
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