using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderApi.Data
{
    public class UsersA
    {
        public static Dictionary<string, string[]> users = new Dictionary<string, string[]>
        {
            {"Company1", new string[]{"userA1", "pswdA1", "encrKeyA1"} },
            {"Company2", new string[]{"userA2", "pswdA2", "encrKeyA2"} },
            {"Company3", new string[]{"userA3", "pswdA3", "encrKeyA3"} },
            {"Cymax", new string[]{"user1", "pswd1", "encrKey1"} }
        };
    }
}
