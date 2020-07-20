using NetsmartzOnlinePortal.Database.DAL;
using NetsmartzOnlinePortal.Database.IRepository;
using NetsmartzOnlinePortal.Database.Repository;
using NetsmartzOnlinePortal.Domain.Model;
using NetsmartzOnlinePortal.Service.INetsmartzOnlinePortalService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetsmartzOnlinePortal.Service.NetsmartzOnlinePortalService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;

        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }

        public int AddUpdateCategory(CategoryModel categoryModel)
        {
            if (categoryModel.CategoryId > 0)
            {
                categoryModel.UpdatedOn = DateTime.Now;
            }
            else
            {
                categoryModel.CreatedOn = DateTime.Now;
                categoryModel.IsActive = true;
            }

            var category = AutoMapper.Mapper.Map<CategoryModel, Category>(categoryModel);

            int result = categoryRepository.AddUpdateCategory(category);

            return result;
        }

        public int DeleteCatById(int categoryId)
        {
            return categoryRepository.DeleteCatById(categoryId);
        }

        public List<CategoryModel> GetAllActiveCategory()
        {
            IEnumerable<Category> lstCategories = categoryRepository.GetAllActiveCategory();
            return AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(lstCategories).ToList();
        }

        public CategoryModel GetCategoryById(int categoryId)
        {
            Category category = categoryRepository.GetCategoryById(categoryId);

            return AutoMapper.Mapper.Map<Category, CategoryModel>(category);
        }

        public string GetPriceRangeByCatId(int categoryId)
        {
            Category category = categoryRepository.GetCategoryById(categoryId);
            string priceRange = "";
            if (category != null)
            {
                priceRange = category.MinPrice + "-" + category.MaxPrice;
            }
            return priceRange;

        }
    }
}
