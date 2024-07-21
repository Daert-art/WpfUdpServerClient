namespace WpfUdpServerClient.UseCases
{
    /// <summary>
    /// Interface for retrieving component prices.
    /// </summary>
    public interface IComponentPriceService
    {
        /// <summary>
        /// Gets the price of a specified component.
        /// </summary>
        /// <param name="componentName">The name of the component.</param>
        /// <returns>The price of the component as a string.</returns>
        string GetComponentPrice(string componentName);

        /// <summary>
        /// Asynchronously gets the price of a specified component.
        /// </summary>
        /// <param name="componentName">The name of the component.</param>
        /// <returns>A task that represents the asynchronous operation, with a string result containing the price of the component.</returns>
        Task<string> GetComponentPriceAsync(string componentName);
        Dictionary<string, string> GetAllPrices();
    }
}
