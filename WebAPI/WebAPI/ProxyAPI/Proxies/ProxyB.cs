using ProxyAPI.Consts;
using System.Collections.Generic;

namespace ProxyAPI.Proxies
{
    public class ProxyB : BaseProxy, IProxyB
    {
        public override string url => $"{Urls.rootUrl}providerB/quote";
        public override Dictionary<string, string> map => new Dictionary<string, string> { { "amount", "price" } };
    }
}
