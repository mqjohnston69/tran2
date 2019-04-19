using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.Web.Http;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.SubSystems.Configuration;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.Common.Implementations;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DataAccess.Implementations;
using System.Web.Http.Controllers;
using Oc.Carbon.ServiceLayer.Contracts;
using Oc.Carbon.ServiceLayer.Implementations;
using Oc.Carbon.Data.Contracts;
using Oc.Carbon.Data.Implementations;


namespace Oc.Carbon.WebServices
{
    public class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            // This "not has component" part is from "Pro ASP.NET Web API" p. 426
            if (!_container.Kernel.HasComponent(serviceType))
            {
                return new object[0];
            }

            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }

    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            this._container = container;
            this._scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }

    public class ApiControllersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly() 
             .BasedOn<ApiController>()
             .LifestylePerWebRequest(),

                        Component.For<ILogService>()
                            .ImplementedBy<LogService>()
                            .LifeStyle.PerWebRequest,

                        Component.For<IDatabaseFactory>()
                            .ImplementedBy<DatabaseFactory>()
                            .LifeStyle.PerWebRequest,

                        Component.For<IUnitOfWork>()
                            .ImplementedBy<UnitOfWork>()
                            .LifeStyle.PerWebRequest,

                        AllTypes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient()//,

                     
                            );
            container.Register(Classes.FromAssemblyNamed("Oc.Carbon.ServiceLayer").Where(type => type.Name.EndsWith("Service")).WithServiceAllInterfaces().LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyNamed("Oc.Carbon.Data").Where(type => type.Name.EndsWith("Repository")).WithServiceAllInterfaces().LifestylePerWebRequest());

        }
    }

}