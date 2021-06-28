using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProxyAPI.Consts;

namespace ProxyAPI.Proxies
{
    public class ProxyC : BaseProxy, IProxyC
    {
        public override string url => $"{Urls.rootUrl}providerC/quote";
        public override Dictionary<string, string> map => new Dictionary<string, string> { {"xml","Quote" }, { "quote", "price" } };

        public override Quote GetQuote(int maxPrice, int x, int y, int z)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return ConvertToQuote(MapFieldsToQuote(response.Content.ReadAsStringAsync().Result));
            }
        }
        public override Quote ConvertToQuote(string strQuote)
        {
            try
            {
                return new XmlSerializer(typeof(Quote)).Deserialize(new StringReader(strQuote)) as Quote;
            }
            catch(Exception e)
            {
                return new Quote { providerId = -1, description = e.Message};
            }
        }

    }
}
