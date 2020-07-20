using NetsmartzOnlinePortal.Service.INetsmartzOnlinePortalService;
using NetsmartzOnlinePortal.Service.NetsmartzOnlinePortalService;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace NetsmartzOnlinePortal
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IProductService, ProductService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}