using System.Collections.Concurrent;
using System.Net;
using WpfUdpServerClient.Entities;

namespace WpfUdpServerClient.UseCases
{
    public class ClientManager : IClientManager
    {
        private readonly ConcurrentDictionary<IPEndPoint, ClientRequestInfo> _clientRequestInfos = new ConcurrentDictionary<IPEndPoint, ClientRequestInfo>();
        private readonly int _maxClients = 100;
        private readonly TimeSpan _inactiveThreshold = TimeSpan.FromMinutes(10);

        public bool TryAddClient(IPEndPoint remoteEP, out ClientRequestInfo clientInfo)
        {
            RemoveInactiveClients();

            if (_clientRequestInfos.Count >= _maxClients)
            {
                clientInfo = null;
                return false;
            }

            clientInfo = _clientRequestInfos.GetOrAdd(remoteEP, _ => new ClientRequestInfo());
            clientInfo.LastActive = DateTime.Now;
            return true;
        }

        public void RemoveInactiveClients()
        {
            DateTime now = DateTime.Now;

            foreach (var entry in _clientRequestInfos.Where(entry => (now - entry.Value.LastActive) > _inactiveThreshold).ToList())
            {
                _clientRequestInfos.TryRemove(entry.Key, out _);
            }
        }

        public bool IsClientActive(IPEndPoint remoteEP)
        {
            return _clientRequestInfos.TryGetValue(remoteEP, out var clientInfo)
                && (DateTime.Now - clientInfo.LastActive) <= _inactiveThreshold;
        }
    }
}
