using System.Configuration;
using System.Data;
using System.Windows;
using WpfUdpServerClient.Loging;

namespace WpfUdpServerClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoggerSetup.ConfigureLogger();

            //    var startupWindow = new StartupWindow();
            //    startupWindow.Show();
        }
    }

}
