
namespace ApiServices.App_Start
{
    using ARQ.Maqueta.Entities;
    using ARQ.Maqueta.Services;
    using Microsoft.Practices.Unity;

    /// <summary>
    ///  The unity config.
    /// </summary>
    public class UnityConfig
    {
        /// <summary>
        /// Configures the specified container.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        internal static void Configure(IUnityContainer container)
        {
            container.RegisterType<IFlightService, FlightService>();
            container.RegisterType<IEntitiesDB, EntitiesDB>();
        }
    }
}