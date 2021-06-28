namespace ProxyAPI.Proxies
{
    public interface IProxyB : IBaseProxy
    {
        string url { get; }

        Quote GetQuote(int maxPrice, int x, int y, int z);
        string MapFieldsToQuote(string src);
    }
}