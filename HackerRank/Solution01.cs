using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    partial class Solution
    {
        static int[] largestPermutation(int k, int[] arr)
        {
            int idx = 0;
            int[] map = arr.Aggregate(new int[arr.Length + 1], (m, n) => { m[n] = idx++; return m; });
            for (idx = 0; idx < arr.Length && k > 0; idx++)
            {
                if (arr[idx] != arr.Length - idx)
                {
                    k--;
                    int tmp = arr[idx];
                    arr[idx] = arr.Length - idx;
                    arr[map[arr.Length - idx]] = tmp;
                    // update map
                    map[tmp] = map[arr.Length - idx];
                    map[arr.Length - idx] = idx;

                }
            }
            return arr;
        }
        static int maximumToys(int[] prices, int k)
        {
            int sum = 0;
            return prices.OrderBy(p => p).Aggregate(0, (cnt, n) =>
            {
                if (sum + n <= k)
                {
                    sum += n;
                    cnt++;
                }
                return cnt;
            });
        }
        static int getMinimumCost(int k, int[] c)
        {
            int m = c.Length / k + 1;
            int cnt = c.Length % k;
            return c.OrderBy(e => e).Aggregate(0, (sum, p) =>
            {
                if (cnt-- == 0) { cnt = k - 1; m--; }
                return sum + p * m;
            });
        }
        static int maxMin(int k, int[] arr)
        {
            int[] ord = arr.OrderBy(a => a).ToArray();
            int min = int.MaxValue;
            for (int idx = 0; idx <= ord.Length - k; idx++)
            {
                int newMin = ord[idx + k - 1] - ord[idx];
                if (newMin < min)
                    min = newMin;
            }
            return min;
        }
        static int[] jimOrders(int[][] orders)
        {
            return orders.Select((e, i) => new int[] { i + 1, e[0] + e[1], e[0] }).
                OrderBy(it => it[1]).ThenBy(it => it[0]).Select(e => e[0]).ToArray();
        }
        static string twoArrays(int k, int[] A, int[] B) // permute [if needed] A & B => A[i]+B[i] >= k
        {
            int[] ordA = A.OrderBy(a => a).ToArray();
            int[] ordB = B.OrderByDescending(b => b).ToArray();
            for (int i = 0; i < A.Length; i++)
            {
                if (ordA[i] + ordB[i] < k)
                    return "NO";
            }
            return "YES";
        }
        static int boardCutting(int[] cost_y, int[] cost_x)
        {
            var ordX = cost_x.OrderByDescending(x => x).ToArray();
            var ordY = cost_y.OrderByDescending(y => y).ToArray();
            int ix = 0, iy = 0;
            long tot = 0;
            long cntX = 1, cntY = 1;
            while (ix < cost_x.Length || iy < cost_y.Length)
            {
                if (ix < cost_x.Length)
                {
                    if (iy >= cost_y.Length || ordX[ix] >= ordY[iy])
                    {
                        tot += ordX[ix++] * cntX;
                        cntY++;
                    }
                    else
                    {
                        tot += ordY[iy++] * cntY;
                        cntX++;
                    }
                }
                else
                {
                    tot += ordY[iy++] * cntY;
                    cntX++;
                }
                if (tot >= 1000000007) tot %= 1000000007;
            }
            return (int)tot;
        }
        static string gameOfThrones(string s) // check if s can be a palindrome
        {
            return s.Length < 2 ? "YES" : s.Length % 2 == 0
                ? (s.ToCharArray().GroupBy(c => c).Count(g => g.Count() % 2 != 0) == 0
                    ? "YES" : "NO")
                : s.ToCharArray().GroupBy(c => c).Count(g => g.Count() % 2 != 0) == 1 ? "YES" : "NO";
        }
        static int makingAnagrams(string s1, string s2) // check if s1 is anagram to s2
        {
            int[] frq = new int[26];
            s1.Aggregate(0, (sum, a) => { frq[a - 'a']++; return sum; });
            s2.Aggregate(0, (sum, b) => { frq[b - 'a']--; return sum; });
            return frq.Aggregate(0, (cnt, n) => cnt + Math.Abs(n));
        }
        static int anagram(string s) // count step left half of the string will be made an anagram of the right one
        {
            if (s.Length % 2 == 1) return -1;
            return s.Substring(0, s.Length / 2)
                .Aggregate(s.Substring(s.Length / 2, s.Length / 2)
                    .Aggregate(new int[26], (asc, c) => { asc[c - 'a']++; return asc; }), 
                    (asc, c) => { asc[c - 'a']--; return asc; })
                .Sum(a => Math.Abs(a)) / 2;
        }
        static int stringConstruction(string s) // minimize copy to a new str, options: 1. copy any char from origin - $1, copy from new str to the end - free
        {
            return s.Aggregate(new int[26], (asc, c) => { asc[c - 'a']++; return asc; }).Sum(n => n > 0 ? 1 : 0);
        }
        static string isValid(string s) // check if 1 char replacement can make all char count even (if not already)
        {
            var radix = s.Aggregate(new int[26], (asc, c) => { asc[c - 'a']++; return asc; }).Where(i => i > 0);
            var chGroups = radix.GroupBy(n => n);
            return chGroups.Count() == 1
                || chGroups.Count() == 2 && chGroups.Any(g => g.Count() == 1) 
                && (chGroups.Any(g => g.Count() == 1 && g.ElementAt(0) < 2) || chGroups.Max(g=>g.ElementAt(0)) - chGroups.Min(g=>g.ElementAt(0)) < 2)
                ? "YES"
                : "NO";
        }
        static string highestValuePalindrome(string s, int k)
        {
            // fill out all asymmetries, if k exceeded before: -1
            int len = s.Length, nAsym = 0;
            char[] arr = s.ToCharArray();
            bool[] asym = new bool[len / 2];
            for (int i = 0; i < len / 2; i++)
            {
                if (s[i] != arr[len - i - 1])
                {
                    k--;
                    asym[i] = true;
                    if (k < 0) return "-1";
                    if (arr[i] - arr[len - i - 1] > 0)
                    {
                        arr[len - i - 1] = arr[i];
                    }
                    else
                    {
                        arr[i] = arr[len - i - 1];
                    }
                }
            }
            // replace non-9 in pairs s[i], s[len - i - 1], i = 0...len
            for (int i = 0; i < len / 2 && k >= (asym[i] ? 1 : 2); i++)
            {
                if (arr[i] != '9' && (k -= asym[i] ? 1 : 2) >= 0)
                {
                    arr[i] = arr[len - i - 1] = '9';
                }
            }
            // if the length is odd and only 1 replacement left - only mid-char can serve
            if (k >= 1 && len % 2 == 1 && arr[len / 2] != '9')
                arr[len / 2] = '9';
            return string.Join("", arr);
        }
    }
}
