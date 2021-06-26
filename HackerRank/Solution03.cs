using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace HackerRank
{
    partial class Solution
    {
        public static int substrings(string str)
        {
            int len = str.Length, idx = 0, mod = 1000000009;
            BigInteger fctr = BigInteger.Parse(new string('1', len));
            return (int)(str.Aggregate((BigInteger)0, (sum, c) =>
            {
                sum += (idx + 1) * fctr * (c-'0');
                idx++;
                fctr /= 10;
                return sum;
            }) % mod);
            //int l = s.Length, MOD = 1000000009;
            //long res = 0;
            //long f = 1;
            //for (int i = l - 1; i >= 0; i--)
            //{
            //    res = (res + (s[i] - '0') * f * (i + 1)) % MOD;
            //    f = (f * 10 + 1) % MOD;
            //}
            //return res;
            //int len = str.Length, idx = 0;
            //BigInteger sum = 0, mod = 1000000009;
            //foreach (var c in str)
            //{
            //    var bi = BigInteger.Parse(new string(c, len - idx));
            //    sum += (idx + 1) * bi;
            //    idx++;
            //}
            //return (int)(sum % mod);
        }
    }
}
