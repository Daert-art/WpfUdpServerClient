using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using WpfUdpServerClient.Interfaces;

namespace WpfUdpServerClient.Infrastructure
{
    public class CustomUdpClient : IClient
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _serverEndPoint;

        public CustomUdpClient()
        {
            _udpClient = new UdpClient();
            _serverEndPoint = new IPEndPoint(IPAddress.Loopback, 8080);
            _udpClient.Connect(_serverEndPoint);
        }

        public async Task StartAsync()
        {
            await Task.CompletedTask;
        }

        public async Task SendRequestAsync(string componentName)
        {
            bool success = false;
            int attempt = 0;
            int maxAttempts = 5;
            TimeSpan delay = TimeSpan.FromSeconds(2);

            while (attempt < maxAttempts && !success)
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(componentName);
                    await _udpClient.SendAsync(data, data.Length);

                    UdpReceiveResult receiveResult = await _udpClient.ReceiveAsync();
                    string response = Encoding.UTF8.GetString(receiveResult.Buffer);

                    MessageBox.Show($"Server response: {response}");
                    success = true;
                }
                catch (SocketException)
                {
                    MessageBox.Show("Server not reachable. Retrying...");
                    attempt++;
                    await Task.Delay(delay);
                }
            }

            if (!success)
            {
                MessageBox.Show("Failed to contact the server after several attempts.");
            }
        }
    }
}
