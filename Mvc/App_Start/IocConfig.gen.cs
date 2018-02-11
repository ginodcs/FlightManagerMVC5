using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using ARQ.Maqueta.Entities;
using ARQ.Maqueta.Services;

[assembly: PreApplicationStartMethod(typeof(ARQ.Maqueta.Presentation.Mvc.IocConfig), "Start")]

namespace ARQ.Maqueta.Presentation.Mvc
{
    public static partial class IocConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });
    
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IEntitiesDB, EntitiesDB>(new PerRequestLifetimeManager());			
            container.RegisterType<IUsuarioService, UsuarioService>(new PerRequestLifetimeManager());
            container.RegisterType<IDepartamentoService, DepartamentoService>(new PerRequestLifetimeManager());

            RegisterCustomTypes(container);
		}
        
        public static void Start()
        {
            var container = IocConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // We are using PerRequestLifetimeManager, so we need to install the module
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        static partial void RegisterCustomTypes(IUnityContainer container);

    }
}