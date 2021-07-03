using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyAPI.Consts
{
    public class Creds
    {
        public static string[] companyIDs = new string[] { "companyA", "companyB", "companyC" };
        public static Dictionary<string, string[]> credRepo = new Dictionary<string, string[]>
        {
            { companyIDs[0], new string[]{"user1", "pswd1", "encrKey1"} },
            { companyIDs[1], new string[]{"user2", "pswd2", "encrKey2"} },
            { companyIDs[2], new string[]{"user3", "pswd3", "encrKey3"} }
        };
    }
}
