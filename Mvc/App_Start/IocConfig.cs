using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ARQ.Maqueta.Presentation.Mvc
{
    public static partial class IocConfig
    {
        static partial void RegisterCustomTypes(IUnityContainer container)
        {
            // Register your custom types here, e.g:
            //  container.RegisterType<IFeatureService, FeatureService>(new HierarchicalLifetimeManager());
            //
            // Or by using Unity Conventions, e.g:
            //
            // container.RegisterTypes(
            //    AllClasses.FromLoadedAssemblies()
            //              .Where(c => c.Namespace == "ARQ.Maqueta.Services"),
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Custom<PerRequestLifetimeManager>
            //);
        }
    }
}