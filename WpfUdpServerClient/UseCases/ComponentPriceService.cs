using System.Collections.ObjectModel;

namespace WpfUdpServerClient.UseCases
{
    public class ComponentPriceService : IComponentPriceService
    {
        private readonly Dictionary<string, string> _componentsPrices = new Dictionary<string, string>()
        {
            { "processor", "200 USD" },
            { "graphics card", "500 USD" },
            { "motherboard", "150 USD" },
            { "ram", "80 USD" },
            { "hard drive", "100 USD" }
        };

        public string GetComponentPrice(string componentName)
        {
            if (string.IsNullOrWhiteSpace(componentName))
            {
                return "Invalid component name";
            }

            return _componentsPrices.TryGetValue(componentName.ToLower(), out var price)
                ? price
                : "Component not found";
        }

        public async Task<string> GetComponentPriceAsync(string componentName)
        {
            return await Task.Run(() => GetComponentPrice(componentName));
        }
        public Dictionary<string, string> GetAllPrices()
        {
            return _componentsPrices;
        }
    }
}
