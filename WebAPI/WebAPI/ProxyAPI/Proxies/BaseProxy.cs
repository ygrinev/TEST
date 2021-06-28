using Newtonsoft.Json;
using ProxyAPI.Consts;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProxyAPI.Proxies
{
    public class BaseProxy : IBaseProxy
    {
        public virtual string url => Urls.rootUrl;
        public virtual Dictionary<string, string> map => new Dictionary<string, string>();
        public virtual Quote GetQuote(int maxPrice, int x, int y, int z)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return JsonConvert.DeserializeObject<Quote>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public virtual string MapFieldsToQuote(string src)
        {
            return map.Aggregate(src, (res, cur) => Regex.Replace(res ?? "", @$"\b{cur.Key}\b", cur.Value));
        }

        public virtual Quote ConvertToQuote(string strQuote)
        {
            return JsonConvert.DeserializeObject<Quote>(strQuote);
        }
    }
}