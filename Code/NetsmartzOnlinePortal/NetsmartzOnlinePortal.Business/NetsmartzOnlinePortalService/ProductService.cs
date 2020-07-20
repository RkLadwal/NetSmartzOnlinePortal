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
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }


        public int AddUpdateProduct(ProductModel ProductModel)
        {
            if (ProductModel.ProductId > 0)
            {
                ProductModel.UpdatedOn = DateTime.Now;
            }
            else
            {
                ProductModel.CreatedOn = DateTime.Now;
                ProductModel.IsActive = true;
            }

            var Product = AutoMapper.Mapper.Map<ProductModel, Product>(ProductModel);

            int result = _productRepository.AddUpdateProduct(Product);

            return result;
        }

        public int DeleteProductById(int ProductId)
        {
            return _productRepository.DeleteProductById(ProductId);
        }

        public List<ProductModel> GetAllActiveProduct()
        {
            IEnumerable<Product> lstCategories = _productRepository.GetAllActiveProducts();
            return AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(lstCategories).ToList();
        }

        public ProductModel GetProductById(int ProductId)
        {
            Product Product = _productRepository.GetProductById(ProductId);

            return AutoMapper.Mapper.Map<Product, ProductModel>(Product);
        }
    }
}
