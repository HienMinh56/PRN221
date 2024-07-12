using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        public Category GetCategoryById(string cateId);
        public List<Category> GetCategories();
    }
}
