using System.Collections.ObjectModel;

namespace WpfUdpServerClient.UseCases
{
    public class ComponentPriceService : IComponentPriceService
    {
        private readonly ReadOnlyDictionary<string, string> _componentsPrices;

        public ComponentPriceService()
        {
            var componentsPrices = new Dictionary<string, string>
            {
                { "processor", "200 USD" },
                { "graphics card", "500 USD" },
                { "motherboard", "150 USD" },
                { "ram", "80 USD" },
                { "hard drive", "100 USD" }
            };

            _componentsPrices = new ReadOnlyDictionary<string, string>(componentsPrices);
        }

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
    }
}
