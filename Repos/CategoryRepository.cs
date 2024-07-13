using BOs.Entities;
using DAOs;
using Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO = null;
        public CategoryRepository()
        {
            if (_categoryDAO == null)
            {
                _categoryDAO = new CategoryDAO();
            }
        }
        public List<Category> GetCategories() => _categoryDAO.GetCategories();

        public Category GetCategoryById(string cateid) => _categoryDAO.GetCategoryById(cateid);
    }
}