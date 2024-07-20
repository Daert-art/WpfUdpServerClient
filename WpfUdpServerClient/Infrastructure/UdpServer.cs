using System.Net;
using System.Net.Sockets;
using System.Text;
using WpfUdpServerClient.Interfaces;
using WpfUdpServerClient.UseCases;

namespace WpfUdpServerClient.Infrastructure
{
    public class UdpServer : IServer
    {
        private readonly UdpClient _udpClient;
        private readonly HandleClientRequestUseCase _handleClientRequestUseCase;
        private readonly IClientManager _clientManager;
        private bool _isRunning;

        public UdpServer(HandleClientRequestUseCase handleClientRequestUseCase, IClientManager clientManager)
        {
            _udpClient = new UdpClient(8080);
            _handleClientRequestUseCase = handleClientRequestUseCase;
            _clientManager = clientManager;
            _isRunning = false;
        }

        public void Start()
        {
            if (_isRunning)
            {
                Console.WriteLine("Server is already running.");
                return;
            }

            _isRunning = true;
            Task.Run(() => ListenForClients());
        }

        private async Task ListenForClients()
        {
            Console.WriteLine("Server started...");
            while (_isRunning)
            {
                try
                {
                    UdpReceiveResult receiveResult = await _udpClient.ReceiveAsync();
                    IPEndPoint remoteEP = receiveResult.RemoteEndPoint;
                    string componentName = Encoding.UTF8.GetString(receiveResult.Buffer);

                    if (_clientManager.TryAddClient(remoteEP, out var clientInfo))
                    {
                        clientInfo.LastActive = DateTime.Now;
                        string response = _handleClientRequestUseCase.HandleRequest(remoteEP, componentName, clientInfo);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await _udpClient.SendAsync(responseBytes, responseBytes.Length, remoteEP);
                    }
                    else
                    {
                        string response = "Server is full. Please try again later.";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await _udpClient.SendAsync(responseBytes, responseBytes.Length, remoteEP);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _udpClient.Close();
        }
    }
}
