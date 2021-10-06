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
            return b == 0 ? 1 : b == 1 ? a : b % 2 == 1 ? (int)(powMod(a, b - 1, MOD) * 1L * a % MOD) : (int)(powMod(a, b / 2, MOD) * 1L * powMod(a, b / 2, MOD) % MOD);
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
                a = (int)(a * 1L * decMod(allWays, i - 1, MOD) % MOD);
            }
            return decMod(a, dp[n], MOD);
        }
        private static int[] primeCnt;
        private static void initPrimes2(int n)
        {
            bool[] primeIdx = new bool[Math.Max(n + 1, 4)]; primeIdx[0] = true; primeIdx[1] = true;
            int curPrimeCnt = 1;
            primeCnt = new int[n + 1]; primeCnt[2] = curPrimeCnt;
            for (int i = 3; i < n; i += 2)
            {
                if (!primeIdx[i]) // i is a prime
                {
                    curPrimeCnt++;
                    for (int delta = i * 2, j = i + delta; j < n + 1; j += delta)
                        primeIdx[j] = true;
                }
                primeCnt[i + 1] = primeCnt[i] = curPrimeCnt;
            }
        }
        public static IEnumerable<int> redJohn(List<int> src)
        {
            int idx = -1;
            int[] perms = new int[41].Aggregate(new int[41], (arr, cur) => {
                arr[++idx] = idx < 4 ? 1 : arr[idx - 1] + arr[idx - 4];
                return arr;
            });
            initPrimes2(perms.Max());
            return src.Select(n => primeCnt[perms[n]]);
        }
        public static long bricksGame(List<int> arr, int N = 3)
        {
            if (arr.Count <= N) return arr.Sum();
            arr.Reverse();
            Queue<long> lastNSlns = new Queue<long>(arr.Take(N).Select((n, i) => (long)arr.Take(i + 1).Sum()));
            arr.Skip(N).Aggregate((long)arr.Take(N).Sum(), (sum, k) =>
            {
                sum += k;
                lastNSlns.Enqueue(sum - lastNSlns.Min());
                lastNSlns.Dequeue();
                return sum;
            });
            return lastNSlns.ElementAt(N - 1);
        }
        public static int longestIncreasingSubsequence(List<int> arr) // 2 7 4 3 8
        {
            int nextIdx = 0;
            return arr.Aggregate(new int[arr.Count + 1], (idxs, cur) => {
                if (cur > idxs[nextIdx]) idxs[++nextIdx] = cur;
                else if (cur < idxs[nextIdx])
                {
                    // change next bigger value to cur
                    int left = 0, right = nextIdx;
                    while (left < right - 1)
                    {
                        int newidx = (right + left) / 2;
                        if (idxs[newidx] >= cur) right = newidx;
                        else left = newidx;
                        if (idxs[newidx] == cur) break;
                    }
                    idxs[right] = cur;
                }
                return idxs;
            }).Where(n => n > 0).Count(); // O(N*Log(N))
            //return arr.Aggregate(new int[arr.Max() + 1], (idxs, cur) => {
            //    idxs[cur] = cur == 1 ? 1 : idxs.Take(cur).Max()+1;
            //    return idxs;
            //}).Max(); // O(N^2)
        }
        static int longestCommonLooseSubsstring(int k, string a, string b)
        {
            return Math.Min(commonChild(a, b) + k, Math.Min(a.Length, b.Length));
        }
        public static List<List<string>> searchSuggestions(List<string> repository, string customerQuery)
        {
            if ((customerQuery?.Length ?? 0) < 2) return new List<List<string>>();
            int minLen = 2, curLen = minLen;
            string curStr = customerQuery.Substring(0, curLen).ToUpper();
            var lst = repository; //.Where(r=>(r??"").ToUpper().IndexOf(curStr) == 0).ToList();
            var res = new List<List<string>>(customerQuery.Length - minLen + 1);
            while (curLen <= customerQuery.Length)
            {
                curStr = customerQuery.Substring(0, curLen).ToUpper();
                lst = lst.Where(r => (r ?? "").ToUpper().IndexOf(curStr) == 0).OrderBy(r => r).ToList();
                res.Add(lst);
                curLen++;
            }
            return res.ToList();
        }
        // Gratest Common Divisor
        public static int GCD(int a, int b)
        {
            return a == 0 ? b : GCD(b%a, a);
        }

        public static List<int> absolutePermutation(int n, int k) // get min array of int from 1..n by shifting each element to k pos
        {
            return k == 0 ? Enumerable.Repeat(0, n).Aggregate(new List<int>(), (lst, cur) => {
                lst.Add(++k); return lst;
            }) : n % (2 * k) != 0 ? new List<int> { -1 } : GetSwap(n, n / (2 * k));
        }
        private static List<int> GetSwap(int n, int factor)
        {
            int k = n / (2 * factor);
            List<int> ret = new List<int>();
            for (int i = 0; i < factor; i++)
            {
                for (int j = i * 2 * k + 1; j <= i * 2 * k + 2 * k; j++)
                {
                    ret.Add(j <= i * 2 * k + k ? j + k : j - k);
                }
            }
            return ret;
        }
        public static int gameWithCells(int n, int m) // num of drops to partially cover all cells in n*n area
        {
            return (n / 2) * (m / 2) + (n % 2) * m / 2 + (m % 2) * n / 2 + (m * n) % 2;
        }
        public static int primeCount(long n)
        {
            int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 }; // first 20 - enough for any long
            long max = long.MaxValue - 1;
            int cnt = 0;
            primes.Aggregate(1L, (acc, cur) =>
            {
                if (n >= acc)
                {
                    cnt++;
                    acc = max / cur <= acc ? max : acc * cur;
                }
                return acc;
            });
            return cnt - 1;
        }
        public static List<double> movingTiles(long l, long s1, long s2, List<long> queries)
        {
            double factor = Math.Cos(Math.PI / 4);
            double vDiff = Math.Abs(s1 - s2);
            return queries.Select(q => 2 * (l - Math.Sqrt(q)) / vDiff * factor).ToList();
        }
        public static int playWithWords(string s)
        {
            if (s.Length < 2) return s.Length;
            int[,] mtx = new int[s.Length, s.Length];
            for (int i = 0; i < s.Length; i++) mtx[i, s.Length - i - 1] = 1;
            for (int j = s.Length - 2; j >= 0; j--)
            {
                for (int k = 0; k <= j; k++)
                {
                    mtx[k, j - k] = s[k] == s[k + (s.Length - j - 1)]
                        ? mtx[k + 1, j - k + 1] + 2
                        : new List<int> { mtx[k + 1, j - k], mtx[k, j - k + 1], mtx[k + 1, j - k + 1] }.Max();
                }
            }
            int max = 1;
            for (int l = 0; l < s.Length - 1; l++)
            {
                int newVal = mtx[0, s.Length - l - 1] * mtx[l + 1, 0];
                if (newVal > max) max = newVal;
            }
            return max;
        }
        //Test cases:
        //1. Input: aaabccccdd
        //Output: a3bc4d2
        //2. Input: yyyyyzzzzzzzzzz
        //Output: y5z10
        //3. Input: abcd
        //Output: abcd(note: not a1b1c1d1)
        //4. Input: hhhccchhhccc
        //Output: h3c3h3c3
        public static string compress(string s)
        {   // assume the string containing only low-case english letters 
            if ((s ?? "").Length < 2) return s;
            char prevChar = '\0';
            int count = 0;
            return s.Aggregate(new StringBuilder(), (sb, c) => {
                if (prevChar == c) count++;
                else
                {
                    sb.Append($"{(count == 0 ? "" : prevChar.ToString())}{(count > 1 ? count.ToString() : "")}");
                    prevChar = c;
                    count = 1;
                }
                return sb;
            }).Append($"{s[s.Length - 1]}{(count > 1 ? count.ToString() : "")}").ToString();
        }
        private static int NOD(int l, int b)
        {
            int min = Math.Min(l, b), max = Math.Max(l, b);
            return min == 1 || max % min == 0 ? min : NOD(min, max % min);
        }
        public static int restaurant(int l, int b)
        {
            int nod = NOD(l, b);
            return l / nod * b / nod;
        }
        public static string bobAndBen(List<List<int>> trees)
        {
            return trees.Aggregate(0, (xor, t) => xor ^ (t[0] == 0 || t[0] == 2 ? 0 : (t[0] - 1) % 2 + 1)) == 0
            ? "BEN" : "BOB";
        }
        public static int countEvenDivisors(int n) // find even divs of n - correct but slow
        {
            int factor = 0;
            int tmp = n;
            for (; tmp % 2 == 0; tmp /= 2) factor++;

            List<int> primes = primes = new List<int> { 3, 5, 7, 11, 13, 17, 19 };
            for (int i = 23; i <= tmp; i += 2)
            {
                if (!primes.Any(p => i % p == 0))
                    primes.Add(i);
            }
            if (factor == 0) return 0;
            int divs = primes.Where(p => p <= tmp).Aggregate(factor, (cnt, p) => {
                int tmpCnt = 1;
                while (tmp % p == 0)
                {
                    tmp /= p;
                    tmpCnt++;
                }
                return cnt * tmpCnt;
            });
            return divs;
        }
        public static int countEvenDivisors2(int n) // find even divs of n - correct and fast, but primitive
        {
            int ans = 1;
            if (n % 2 != 0) return 0;
            for (int i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    if (i % 2 == 0) ans++;
                    if ((n / i) % 2 == 0) ans++;
                    if (i == n / i) ans--;
                }
            }
            return ans;
        }
        private static int getLCT(int k, string a, string b, int i, int j)
        {
            int max = 0, curLen = 0, acc = 0;
            for (int offs0 = 0, offs1 = 0; i + offs1 < a.Length && j + offs1 < b.Length; offs1++)
            {
                if (a[i + offs1] == b[j + offs1] || ++acc <= k) curLen++;
                else
                {
                    max = Math.Max(curLen, max);
                    while (offs0 < offs1 && a[i + offs0] == b[j + offs0]) offs0++;
                    if (offs0 < offs1) offs0++;
                    acc--;
                    curLen = offs1 - offs0 + 1;
                }
            }
            max = Math.Max(curLen, max);
            return max;
        }
        public static int substringDiff(int k, string a, string b)
        {
            int max = 0;
            for (int j = 0; j < b.Length && max < Math.Min(a.Length, b.Length - j); j++)
            {
                max = Math.Max(max, getLCT(k, a, b, 0, j));
            }
            for (int i = 1; i < a.Length && max < Math.Min(b.Length, a.Length - i); i++)
            {
                max = Math.Max(max, getLCT(k, a, b, i, 0));
            }

            return max;
        }
    }
}
