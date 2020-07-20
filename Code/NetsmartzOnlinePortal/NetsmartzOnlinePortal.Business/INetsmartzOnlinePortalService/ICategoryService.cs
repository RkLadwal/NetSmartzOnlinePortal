using NetsmartzOnlinePortal.Domain.Model;
using System.Collections.Generic;

namespace NetsmartzOnlinePortal.Service.INetsmartzOnlinePortalService
{
    public interface ICategoryService
    {
        int AddUpdateCategory(CategoryModel category);
        List<CategoryModel> GetAllActiveCategory();
        CategoryModel GetCategoryById(int categoryId);
        int DeleteCatById(int categoryId);
        string GetPriceRangeByCatId(int categoryId);
    }
}
