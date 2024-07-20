using System.Windows;
using WpfUdpServerClient.Infrastructure;
using WpfUdpServerClient.UseCases;
using WpfUdpServerClient.Interfaces;

namespace WpfUdpServerClient
{
    public partial class ServerWindow : Window
    {
        private UdpServer _server;

        public ServerWindow()
        {
            InitializeComponent();
            var componentPriceService = new ComponentPriceService();
            var requestLimiter = new RequestLimiter();
            var clientManager = new ClientManager();
            var handleClientRequestUseCase = new HandleClientRequestUseCase(requestLimiter, componentPriceService);
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
    }
}