using NetsmartzOnlinePortal.Database.DAL;
using NetsmartzOnlinePortal.Database.IRepository;
using NetsmartzOnlinePortal.Domain.NetSmartzPortalEnum;
using System.Collections.Generic;
using System.Linq;

namespace NetsmartzOnlinePortal.Database.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private NetsmartzPortalEntities _context;

        public CategoryRepository()
        {
            _context = new NetsmartzPortalEntities();
        }

        public int AddUpdateCategory(Category category)
        {
            if (category.CategoryId > 0)
            {
                Category fetchCat = _context.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                fetchCat.CategoryName = category.CategoryName;
                fetchCat.Description = category.Description;
                fetchCat.MinPrice = category.MinPrice;
                fetchCat.MaxPrice = category.MaxPrice;
                fetchCat.UpdatedOn = category.UpdatedOn;
                _context.SaveChanges();
                return (int)ResultStatus.Updated;
            }
            else
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return (int)ResultStatus.Added;
            }
        }

        public int DeleteCatById(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                category.IsActive = false;
                _context.SaveChanges();
                return (int)ResultStatus.Deleted;
            }
            return (int)ResultStatus.NotFound;
        }

        public IEnumerable<Category> GetAllActiveCategory()
        {
            return _context.Categories.Where(x => x.IsActive.Value);
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
        }
    }
}
