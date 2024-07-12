using BOs.Entities;
using Repos;
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
        private readonly CategoryRepository categoryRepository;

        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }

        public Category GetCategoryById(string cateId)
        {
            return categoryRepository.GetCategoryById(cateId);
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetCategories();
        }
    }
}
