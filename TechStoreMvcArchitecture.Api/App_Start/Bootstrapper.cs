using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using TechStoreMvcArchitecture.Api.Infrastructure;
using TechStoreMvcArchitecture.Data.Infrastructure;
using TechStoreMvcArchitecture.Data.Repositories;
using TechStoreMvcArchitecture.Service.Services;

namespace TechStoreMvcArchitecture.Api.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();
            
            // Repositories
            builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //Services
            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //Set the dependency resolver to be Autofac.  
            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiDependencyResolver(container);
        }
    }
}