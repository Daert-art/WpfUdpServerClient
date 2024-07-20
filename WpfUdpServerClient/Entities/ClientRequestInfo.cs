namespace WpfUdpServerClient.Entities
{
    public class ClientRequestInfo
    {
        private readonly List<DateTime> _requests = new List<DateTime>();

        public IReadOnlyList<DateTime> Requests => _requests.AsReadOnly();

        public DateTime LastActive { get; set; }

        public void AddRequest(DateTime requestTime)
        {
            _requests.Add(requestTime);
        }

        public List<DateTime> GetRecentRequests(TimeSpan timeWindow)
        {
            return _requests
                .Where(r => (DateTime.Now - r) <= timeWindow)
                .ToList();
        }
    }
}
