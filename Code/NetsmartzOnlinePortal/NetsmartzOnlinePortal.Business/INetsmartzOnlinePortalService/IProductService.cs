using NetsmartzOnlinePortal.Domain.Model;
using System.Collections.Generic;

namespace NetsmartzOnlinePortal.Service.INetsmartzOnlinePortalService
{
    public interface IProductService
    {
        int AddUpdateProduct(ProductModel productModel);
        int DeleteProductById(int ProductId);
        List<ProductModel> GetAllActiveProduct();
        ProductModel GetProductById(int ProductId);
    }
}
