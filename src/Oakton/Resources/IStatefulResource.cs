using System;
using System.Threading;
using System.Threading.Tasks;
using Oakton.Environment;
using Spectre.Console.Rendering;

namespace Oakton.Resources
{
    /// <summary>
    /// Adapter interface used by Oakton enabled applications to allow
    /// Oakton to setup/teardown/clear the state/check on stateful external
    /// resources of the system like databases or messaging queues
    /// </summary>
    public interface IStatefulResource
    {
        /// <summary>
        /// Check whether the configuration for this resource is valid. An exception
        /// should be thrown if the check is invalid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task Check(CancellationToken token);
        
        /// <summary>
        /// Clear any persisted state within this resource
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ClearState(CancellationToken token);
        
        /// <summary>
        /// Tear down the stateful resource represented by this implementation
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task Teardown(CancellationToken token);
        
        /// <summary>
        /// Make any necessary configuration to this stateful resource
        /// to make the system function correctly
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task Setup(CancellationToken token);
        
        /// <summary>
        /// Optionally return a report of the current state of this resource
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IRenderable> DetermineStatus(CancellationToken token);
        
        /// <summary>
        /// Categorical type name of this resource for filtering
        /// </summary>
        string Type { get; }
        
        /// <summary>
        /// Identifier for this resource
        /// </summary>
        string Name { get; }
    }

    internal class ResourceEnvironmentCheck : IEnvironmentCheck
    {
        private readonly IStatefulResource _resource;

        public ResourceEnvironmentCheck(IStatefulResource resource)
        {
            _resource = resource;
        }

        public string Description => $"Resource {_resource.Name} ({_resource.Type})";
        public Task Assert(IServiceProvider services, CancellationToken cancellation)
        {
            return _resource.Check(cancellation);
        }
    }
}