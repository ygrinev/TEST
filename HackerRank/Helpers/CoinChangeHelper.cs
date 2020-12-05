using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class CoinChangeHelper
    {
        private static long cntTailWaysSlow(long m, long[] arr, int idx, long[][] done)
        {
            long count = 0;
            long coin = arr[idx];
            if (idx == arr.Length - 1) return coin == 1 || m % coin == 0 ? 1 : 0;
            do
            {
                count += m == 0 ? 1 : cntTailWays(m, arr, idx + 1, done);
            }
            while ((m -= coin) >= 0);
            return count;
        }
        private static long cntTailWays(long n, long[] c, int idx, long[][] done)
        {
            if (n == 0) return 1;
            else if (n < 0) return 0;
            else if (idx < 0) return 0;
            if (done[n][idx] == -1)
                done[n][idx] = cntTailWays(n - c[idx], c, idx, done)
                            + cntTailWays(n, c, idx - 1, done);
            return done[n][idx];
        }
        public static long countChangeWays(int n, List<long> c)
        {
            long[] ord = c.Where(a=>a <= n).OrderBy(e => e).ToArray();
            long[][] done = new long[n+1][];
            for (int i = 0; i <= n; i++) done[i] = Enumerable.Repeat(-1L, ord.Length).ToArray();
            if (ord.Length == 0) return 0;

            return cntTailWays(n, ord, ord.Length - 1, done);
        }
    }
}
