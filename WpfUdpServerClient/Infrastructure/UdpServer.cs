using Serilog;
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
                Log.Information("Server is already running.");
                return;
            }

            _isRunning = true;
            Log.Information("Server starting...");
            Task.Run(() => ListenForClients());
        }

        private async Task ListenForClients()
        {
            Log.Information("Server started and listening for clients...");
            while (_isRunning)
            {
                try
                {
                    UdpReceiveResult receiveResult = await _udpClient.ReceiveAsync();
                    IPEndPoint remoteEP = receiveResult.RemoteEndPoint;
                    string componentName = Encoding.UTF8.GetString(receiveResult.Buffer);

                    Log.Information("Received request from {RemoteEndPoint}: {ComponentName}", remoteEP, componentName);

                    if (_clientManager.TryAddClient(remoteEP, out var clientInfo))
                    {
                        clientInfo.LastActive = DateTime.Now;
                        string response = _handleClientRequestUseCase.HandleRequest(remoteEP, componentName, clientInfo);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await _udpClient.SendAsync(responseBytes, responseBytes.Length, remoteEP);

                        Log.Information("Response sent to {RemoteEndPoint}: {Response}", remoteEP, response);
                    }
                    else
                    {
                        string response = "Server is full. Please try again later.";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await _udpClient.SendAsync(responseBytes, responseBytes.Length, remoteEP);

                        Log.Warning("Client {RemoteEndPoint} rejected: {Response}", remoteEP, response);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Exception occurred while processing request.");
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _udpClient.Close();
            Log.Information("Server stopped.");
        }
    }
}
