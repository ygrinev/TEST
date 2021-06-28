namespace ProxyAPI.Proxies
{
    public interface IProxyC : IBaseProxy
    {
        new string url { get; }

        new Quote GetQuote(int maxPrice, int x, int y, int z);
        string MapFieldsToQuote(string src);
    }
}