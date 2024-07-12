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
        public List<Category> GetCategories()
        {
            return CategoryDAO.Instance.GetCategories();
        }

        public Category GetCategoryById(string cateid)
        {
            return CategoryDAO.Instance.GetCategoryById(cateid);
        }
    }
}