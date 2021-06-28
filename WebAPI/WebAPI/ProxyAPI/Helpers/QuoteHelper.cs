using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProxyAPI.Consts;
using ProxyAPI.Proxies;

namespace ProxyAPI.Helpers
{
    public class QuoteHelper : IQuoteHelper
    {
        private readonly IProxyA _proxyA;
        private readonly IProxyB _proxyB;
        private readonly IProxyC _proxyC;

        public QuoteHelper(IProxyA proxyA, IProxyB proxyB, IProxyC proxyC)
        {
            _proxyA = proxyA;
            _proxyB = proxyB;
            _proxyC = proxyC;
        }
        public Quote GetBestQuote(int maxPrice, int x, int y, int z)
        {
            List<Quote> lstQuotes = new List<Quote> { _proxyA.GetQuote(maxPrice, x, y, z),
                                                      _proxyB.GetQuote(maxPrice, x, y, z),
                                                      _proxyC.GetQuote(maxPrice, x, y, z)
            };
            return GetBestQuote(lstQuotes);
        }

        private Quote GetBestQuote(List<Quote> lstQuotes)
        {
            if (lstQuotes?.Any()??false)
            {
                var nonZero = lstQuotes.Select(q => q?.price ?? 0).Where(p=>p>0);
                int min = nonZero.Count() == 0 ? 0 : nonZero.Min();
                return lstQuotes.FirstOrDefault(q => (q?.price ?? -1) == min);
            }
            return null;
        }
    }
}
