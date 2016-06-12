using AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WebApiUserManagement.Repository;

namespace WebApiUserManagement.App_Start
{
    public static class IocConfig
    {
        public static void Configure(MapperConfiguration mapperConfig)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            container.Register(() => mapperConfig.CreateMapper(container.GetInstance));

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}