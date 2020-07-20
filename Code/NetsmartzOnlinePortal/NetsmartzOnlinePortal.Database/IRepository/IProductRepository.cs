using NetsmartzOnlinePortal.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetsmartzOnlinePortal.Database.IRepository
{
    public interface IProductRepository
    {
        int AddUpdateProduct(Product product);
        int DeleteProductById(int productId);
        IEnumerable<Product> GetAllActiveProducts();
        Product GetProductById(int productId);
    }
}
