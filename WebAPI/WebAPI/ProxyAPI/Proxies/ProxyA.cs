using Newtonsoft.Json;
using ProxyAPI.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProxyAPI.Proxies
{
    public class ProxyA : BaseProxy, IProxyA
    {
        public override string url => $"{Urls.rootUrl}providerA/quote";
        Dictionary<string, string> map = new Dictionary<string, string> { { "total", "price" } };
    }
}
