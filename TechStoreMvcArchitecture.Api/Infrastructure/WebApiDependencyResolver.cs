using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Integration.WebApi;

namespace TechStoreMvcArchitecture.Api.Infrastructure
{
    /// <summary>
    /// Represents a dependency injection container.
    /// </summary>
    public class WebApiDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public WebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }
        /// <summary>Retrieves a service from the scope.</summary>
        /// <returns>The retrieved service.</returns>
        /// <param name="serviceType">The service to be retrieved.</param>
        public object GetService(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ? _container.Resolve(serviceType) : null;
        }
        /// <summary>Retrieves a collection of services from the scope.</summary>
        /// <returns>The retrieved collection of services.</returns>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            Type enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance = _container.Resolve(enumerableServiceType);

            return ((IEnumerable) instance).Cast<object>();
        }

        /// <summary> Starts a resolution scope. </summary>
        /// <returns>The dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            //TODO
            return  new AutofacWebApiDependencyResolver(_container.BeginLifetimeScope());
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
           Dispose(true);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        protected virtual void Dispose(bool disposing)
        {
            _container.Dispose();
        }
    }
}