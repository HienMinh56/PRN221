using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetCategoryById(string cateid);
    }
}
