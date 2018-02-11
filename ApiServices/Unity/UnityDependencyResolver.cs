
namespace ApiServices.Unity
{
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the UnityDependencyResolver type.
    /// </summary>
    /// <seealso cref="Mger.Services.Unity.UnityDependencyScope" />
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var childContainer = this.Container.CreateChildContainer();

            return new UnityDependencyScope(childContainer);
        }
    }
}