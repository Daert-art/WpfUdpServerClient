using WpfUdpServerClient.Entities;

namespace WpfUdpServerClient.UseCases
{
    /// <summary>
    /// Interface for request limiting.
    /// </summary>
    public interface IRequestLimiter
    {
        /// <summary>
        /// Checks if a client's request is allowed based on rate limits.
        /// </summary>
        /// <param name="clientInfo">Information about the client making the request.</param>
        /// <returns>True if the request is allowed; otherwise, false.</returns>
        bool IsRequestAllowed(ClientRequestInfo clientInfo);

        /// <summary>
        /// Asynchronously checks if a client's request is allowed based on rate limits.
        /// </summary>
        /// <param name="clientInfo">Information about the client making the request.</param>
        /// <returns>A task that represents the asynchronous operation, with a boolean result indicating if the request is allowed.</returns>
        Task<bool> IsRequestAllowedAsync(ClientRequestInfo clientInfo);
    }
}
