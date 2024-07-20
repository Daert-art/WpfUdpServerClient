using System.Net;
using WpfUdpServerClient.Entities;

namespace WpfUdpServerClient.UseCases
{
    /// <summary>
    /// Interface for managing client connections.
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// Tries to add a client to the manager.
        /// </summary>
        /// <param name="remoteEP">The endpoint of the client.</param>
        /// <param name="clientInfo">The information about the client.</param>
        /// <returns>True if the client was added successfully; otherwise, false.</returns>
        bool TryAddClient(IPEndPoint remoteEP, out ClientRequestInfo clientInfo);

        /// <summary>
        /// Removes inactive clients from the manager.
        /// </summary>
        void RemoveInactiveClients();

        /// <summary>
        /// Checks if a client is active.
        /// </summary>
        /// <param name="remoteEP">The endpoint of the client.</param>
        /// <returns>True if the client is active; otherwise, false.</returns>
        bool IsClientActive(IPEndPoint remoteEP);
    }
}
