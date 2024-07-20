using WpfUdpServerClient.Entities;

namespace WpfUdpServerClient.UseCases
{
    public class RequestLimiter : IRequestLimiter
    {
        private readonly int _maxRequestsPerHour = 10;
        private readonly TimeSpan _timeWindow = TimeSpan.FromHours(1);

        public bool IsRequestAllowed(ClientRequestInfo clientInfo)
        {
            return IsRequestAllowedInternal(clientInfo);
        }

        public async Task<bool> IsRequestAllowedAsync(ClientRequestInfo clientInfo)
        {
            return await Task.Run(() => IsRequestAllowedInternal(clientInfo));
        }

        private bool IsRequestAllowedInternal(ClientRequestInfo clientInfo)
        {

            var recentRequests = clientInfo.GetRecentRequests(_timeWindow);


            bool isAllowed = recentRequests.Count < _maxRequestsPerHour;


            clientInfo.AddRequest(DateTime.Now);

            return isAllowed;
        }
    }
}
