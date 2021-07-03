using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderApi.Data
{
    public class UsersB
    {
        public static Dictionary<string, string[]> users = new Dictionary<string, string[]>
        {
            {"Company1", new string[]{"userB1", "pswdB1", "encrKeyB1"} },
            {"Company2", new string[]{"userB2", "pswdB2", "encrKeyB2"} },
            {"Company3", new string[]{"userB3", "pswdB3", "encrKeyB3"} },
            {"Cymax", new string[]{"user2", "pswd2", "encrKey2"} }
        };
    }
}
