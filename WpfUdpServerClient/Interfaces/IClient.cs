namespace WpfUdpServerClient.Interfaces
{
    public interface IClient
    {
        Task StartAsync();
        Task SendRequestAsync(string componentName);
    }
}
