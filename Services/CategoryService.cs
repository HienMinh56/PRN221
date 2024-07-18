using BOs.Entities;
using DAOs;
using Repos;
using Repos.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository = null;

        public CategoryService()
        {
            if (categoryRepository == null)
            {
                categoryRepository = new CategoryRepository();
            }

        }

        public Category GetCategoryById(string cateId) => categoryRepository.GetCategoryById(cateId);
        public List<Category> GetCategories()
        {
            return categoryRepository.GetCategories();
        }
    }
}