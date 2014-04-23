using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MaroonVillage.Core.Repositories;

namespace MainWebsite
{
    public class AutofacConfig
    {
        /// <summary>
        /// Register Autofac builder and container.
        /// </summary>
        public static void RegisterBuilder()
        {
            var builder = new ContainerBuilder();

            // Auto register controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Auto register web api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Auto register view model
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterFilterProvider();


            // Auto register all common repositories and services
            var commonAssembly = typeof(BaseRepository).Assembly;
            builder.RegisterAssemblyTypes(commonAssembly)
                   .Where(x => x.Name.EndsWith("repository", StringComparison.OrdinalIgnoreCase) || x.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase))
                   .AsImplementedInterfaces().SingleInstance();

            // Helpers
            //builder.RegisterType<WebHelpers>().SingleInstance();
            //builder.RegisterType<DependencyResolverWrapper>().AsImplementedInterfaces().SingleInstance();
            //builder.RegisterType<IdentityInjectorAttribute>().As<IdentityInjectorAttribute>().PropertiesAutowired().SingleInstance();


            // Dependency resolver
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}