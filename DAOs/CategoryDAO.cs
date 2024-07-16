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
    public class CategoryDAO
    {
        private readonly Dbprn221Context _dbprn221Context;
        private static CategoryDAO instance = null;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public CategoryDAO()
        {
            _dbprn221Context = new Dbprn221Context();
        }

        public List<Category> GetCategories()
        {
            return _dbprn221Context.Categories.ToList();
        }

        public Category GetCategoryById(string cateId)
        {
            return _dbprn221Context.Categories.Find(cateId);
        }
    }
}