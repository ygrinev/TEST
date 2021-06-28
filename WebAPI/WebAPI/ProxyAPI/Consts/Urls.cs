using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyAPI.Consts
{
    public class Urls
    {
        public const string rootUrl = "http://localhost:12733/api/";
        public static string urlA => $"{rootUrl}providerA/quoteA";
        public static string urlB => $"{rootUrl}providerB/quoteB";
    }
}
