using System.Windows;

namespace WpfUdpServerClient
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            var serverWindow = new ServerWindow();
            serverWindow.Show();
            this.Close();
        }

        private void StartClientButton_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new ClientWindow();
            clientWindow.Show();
            this.Close();
        }
    }
}
