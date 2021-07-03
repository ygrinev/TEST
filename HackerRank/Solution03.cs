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
            //int len = str.Length, idx = 0, mod = 1000000007;
            //BigInteger fctr = BigInteger.Parse(new string('1', len));
            //return (int)(str.Aggregate((BigInteger)0, (sum, c) =>
            //{
            //    sum += (idx + 1) * fctr * (c - '0');
            //    idx++;
            //    fctr /= 10;
            //    return sum;
            //}) % mod);
            // TEST
            //long sum = 0L;
            //for(int i = 0; i < len; i++)
            //{
            //    for(int k = 1; k < len - i; k++)
            //    {
            //        long tmp = long.Parse(str.Substring(i, k));
            //        sum  = sum + tmp%mod;
            //    }
            //}
            //return sum%mod;
            int l = str.Length, MOD = 1000000009;
            long res = 0;
            long f = 1;
            for (int i = l - 1; i >= 0; i--)
            {
                res = (res + (str[i] - '0') * f * (i + 1)) % MOD;
                f = (f * 10 + 1) % MOD;
            }
            return (int)res;
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
        public static long stockmax(List<int> prices)
        {
            prices.Reverse();
            int max = prices.ElementAt(0);
            return prices.Aggregate(0L, (s, cur) => {
                if (max >= cur) s += max - cur;
                else max = cur;
                return s;
            });
            //int[] prs = prices.ToArray();
            //int idx = 0;
            //return prices.Select((p, i) => new KeyValuePair<int, int>(i, p)).OrderByDescending(pr => pr.Value).Aggregate(0L, (s, cur) => {
            //    while (++idx <= cur.Key) { s += cur.Value - prs[idx-1]; }
            //    return s;
            //});
        }

        static int powMod(int a, int b, int MOD)
        {
            return b == 0 ? 1 : b == 1 ? a : b % 2 == 1 ? (int)(powMod(a, b - 1, MOD) * 1L* a % MOD) : (int)(powMod(a, b / 2, MOD) * 1L* powMod(a, b / 2, MOD) % MOD);
        }

        static int decMod(int a, int b, int MOD)
        {
            return (a -= b) < 0 ? a + MOD : a;
        }

        public static int tastesLikeWinning(int n, int m)
        {
            int MOD = 1000000000 + 7;
            int MAXN = 10000000 + 10;

                int[] dp = new int[MAXN];

            int a = powMod(2, m, MOD) - 1;
            int allWays = a;
            dp[0] = 1;
            dp[1] = 0;
            for (int i = 2; i <= n; i++)
            {
                dp[i] = decMod(a, dp[i - 1], MOD);
                dp[i] = decMod(dp[i], (int)(dp[i - 2] * 1L * decMod(allWays, (i - 2), MOD) % MOD * 1L * (i - 1) % MOD), MOD);
                a = (int)(a * 1L* decMod(allWays, i -1, MOD) % MOD);
            }
            return decMod(a, dp[n], MOD);
        }
    }
}
