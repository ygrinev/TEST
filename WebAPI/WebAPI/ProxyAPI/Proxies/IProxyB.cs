namespace ProxyAPI.Proxies
{
    public interface IProxyB : IBaseProxy
    {
        new string url { get; }

        new Quote GetQuote(int maxPrice, int x, int y, int z);
        new string MapFieldsToQuote(string src);
    }
}