using QLSinhVien.Models;
using QLSinhVien.Repositories;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace QLSinhVien
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // register DbContext
            container.RegisterType<DbContext, DBSinhVienContext>();

            // register IGenericRepository and GenericRepository
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // register UnitOfWork
            container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}