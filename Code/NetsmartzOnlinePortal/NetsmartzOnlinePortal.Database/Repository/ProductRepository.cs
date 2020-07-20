using NetsmartzOnlinePortal.Database.DAL;
using NetsmartzOnlinePortal.Database.IRepository;
using NetsmartzOnlinePortal.Domain.NetSmartzPortalEnum;
using System.Collections.Generic;
using System.Linq;

namespace NetsmartzOnlinePortal.Database.Repository
{
    public class ProductRepository : IProductRepository
    {
        private NetsmartzPortalEntities _context;

        public ProductRepository()
        {
            _context = new NetsmartzPortalEntities();
        }
        public int AddUpdateProduct(Product product)
        {
            if (product.ProductId > 0)
            {
                Product fetchProduct = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                fetchProduct.CategoryId = product.CategoryId;
                fetchProduct.ProductName = product.ProductName;
                fetchProduct.Description = product.Description;
                fetchProduct.Quantity = product.Quantity;
                fetchProduct.Price = product.Price;
                fetchProduct.Discount = product.Discount;
                fetchProduct.ExpirationDate = product.ExpirationDate;
                fetchProduct.UpdatedOn = product.UpdatedOn;
                _context.SaveChanges();
                return (int)ResultStatus.Updated;
            }
            else
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return (int)ResultStatus.Added;
            }
        }
        public int DeleteProductById(int productId)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                product.IsActive = false;
                _context.SaveChanges();
                return (int)ResultStatus.Deleted;
            }
            return (int)ResultStatus.NotFound;
        }
        public IEnumerable<Product> GetAllActiveProducts()
        {
            IEnumerable<Product> products= _context.Products.Where(x => x.IsActive.Value && x.Category.IsActive.Value);
            return products;
        }
        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(x => x.ProductId == productId);
        }
    }
}
