using System.Windows;
using WpfUdpServerClient.Infrastructure;
using WpfUdpServerClient.UseCases;
using WpfUdpServerClient.Interfaces;
using System.Text;

namespace WpfUdpServerClient
{
    public partial class ServerWindow : Window
    {
        private UdpServer _server;
        private IComponentPriceService _componentPriceService;

        public ServerWindow()
        {
            InitializeComponent();
            _componentPriceService = new ComponentPriceService();
            var requestLimiter = new RequestLimiter();
            var clientManager = new ClientManager();
            var handleClientRequestUseCase = new HandleClientRequestUseCase(requestLimiter, _componentPriceService);
            _server = new UdpServer(handleClientRequestUseCase, clientManager);
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            StartServerButton.IsEnabled = false;
            StopServerButton.IsEnabled = true;
            _server.Start();
        }

        private void StopServerButton_Click(object sender, RoutedEventArgs e)
        {
            _server.Stop();
            StartServerButton.IsEnabled = true;
            StopServerButton.IsEnabled = false;
        }
        private void ShowComponentsList_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentsListTextBox != null)
            {
                var allPrices = _componentPriceService.GetAllPrices();
                var sb = new StringBuilder();

                foreach (var item in allPrices)
                {
                    sb.AppendLine($"{item.Key}: {item.Value}");
                }

                ComponentsListTextBox.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("ComponentsListTextBox is not initialized.");
            }
        }
    }
}