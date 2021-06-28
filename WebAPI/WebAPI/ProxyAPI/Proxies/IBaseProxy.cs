using System.Collections.Generic;

namespace ProxyAPI.Proxies
{
    public interface IBaseProxy
    {
        string url { get; }
        Dictionary<string, string> map { get; }
        Quote GetQuote(int maxPrice, int x, int y, int z);
        Quote ConvertToQuote(string strQuote);
        string MapFieldsToQuote(string src);
    }
}