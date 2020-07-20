using AutoMapper;
using NetsmartzOnlinePortal.Database.DAL;
using NetsmartzOnlinePortal.Domain.Model;

namespace NetsmartzOnlinePortal.Service
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<CategoryModel, Category>().ReverseMap();
            Mapper.CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}
