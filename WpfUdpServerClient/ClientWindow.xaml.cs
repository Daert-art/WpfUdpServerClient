using System.Windows;
using WpfUdpServerClient.Infrastructure;

namespace WpfUdpServerClient
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private readonly CustomUdpClient _client;

        public ClientWindow()
        {
            InitializeComponent();
            _client = new CustomUdpClient();
            InitializeClient();
        }
        private async void InitializeClient()
        {
            await _client.StartAsync();
        }
        private async void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            string componentName = ComponentNameTextBox.Text;
            await _client.SendRequestAsync(componentName);
        }
    }
}
