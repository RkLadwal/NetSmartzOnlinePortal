using NetsmartzOnlinePortal.Database.DAL;
using System.Collections.Generic;

namespace NetsmartzOnlinePortal.Database.IRepository
{
    public interface ICategoryRepository
    {
        int AddUpdateCategory(Category category);
        IEnumerable<Category> GetAllActiveCategory();
        Category GetCategoryById(int categoryId);
        int DeleteCatById(int categoryId);
    }
}
