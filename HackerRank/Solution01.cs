using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            int len = s.Length;
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
        static bool isAnagram(string s, int i1, int i2, int l)
        {
            int[] curSum = new int[26];
            while (--l >= 0)
            {
                curSum[s[i1 + l] - 'a']++;
                curSum[s[i2 + l] - 'a']--;
            }
            return curSum.All(n => n == 0);
        }
        // Complete the sherlockAndAnagrams function below.
        static int sherlockAndAnagrams(string s)
        {
            int[] chStat = s.Aggregate(new int[26], (a, c) => { a[c - 'a']++; return a; });
            int count = chStat.Aggregate(0, (sum, n) => sum + (n < 2 ? 0 : n * (n - 1) / 2));
            for (int l = 2; l < s.Length; l++)
            {
                for (int i = 0; i < s.Length - l; i++)
                {
                    if (chStat[s[i] - 'a'] < 2) continue;
                    for (int j = i + 1; j <= s.Length - l; j++)
                    {
                        if (chStat[s[j + l - 1] - 'a'] < 2)
                        {
                            j += l - 1;
                            continue;
                        }
                        int lInters = Math.Min(j - 1, i + l - 1), rInters = Math.Max(j, i + l);
                        if (isAnagram(s, i, j, l))
                            count++;
                    }
                }
            }
            return count;
        }
        static int commonChild(string a, string b) // Longest common subsequence
        {
            int[,] grf = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])
                    {
                        grf[i + 1, j + 1] = grf[i, j] + 1;
                    }
                    else
                    {
                        grf[i + 1, j + 1] = Math.Max(grf[i + 1, j], grf[i, j + 1]);
                    }
                }
            }
            return grf[a.Length, b.Length];
        }
        static int steadyGene(string g)
        {
            if (g.Length % 4 > 0 || Regex.IsMatch(g, "[^ACGT]")) return -1;
            // find letters which count > n/4
            IEnumerable<int> stat = g.Aggregate(new int[26], (s, c) => { s[c - 'A']++; return s; });
            // create int[26] with differences
            int len = g.Length, dv = len / 4;
            int[] diff = stat.Select(s => (s > dv ? dv : -dv - 1)).ToArray();
            if (!diff.Any(d => d >= 0)) return 0; // all good!!!
            // go back from last idx intil 1 of those "extra" go < 0, stop at next idx[rIdx].
            int rIdx = len - 1;
            for (; rIdx >= 0 && (diff[g[rIdx] - 'A'] >= 0 || diff[g[rIdx] - 'A'] < -dv); rIdx--)
            {
                if (diff[g[rIdx] - 'A'] >= 0)
                {
                    if (diff[g[rIdx] - 'A'] == 0)
                    {
                        rIdx++;
                        break;
                    }
                    diff[g[rIdx]- 'A']--;
                }
            }
            if (rIdx == 0) return 0;
            int lIdx = 0;
            int min = rIdx - lIdx;
            while (rIdx < len)
            {
                // from lIdx = 0 go right until same situation, stop ,
                // rIdx-lIdx - 1st candidate
                for (; lIdx < len && (diff[g[lIdx] - 'A'] >= 0 || diff[g[lIdx] - 'A'] < -dv); lIdx++)
                {
                    if (diff[g[lIdx] - 'A'] >= 0)
                    {
                        if (diff[g[lIdx] - 'A'] == 0)
                            break;
                        --diff[g[lIdx] - 'A'];
                    }
                }
                if (min == 0) return 1;
                if (rIdx - lIdx < min)
                    min = rIdx - lIdx;
                // rIdx++ until the leftmost lost char restored ->lIdx++ 
                // till next char goes negative, stop , rIdx-lIdx - 2nd candidate ->
                //  choose min of last and cur, repeat till rIdx = gene.Length
                while (diff[g[lIdx] - 'A'] == 0 && rIdx < len)
                {
                    if (g[lIdx] - 'A' == g[rIdx] - 'A')
                        diff[g[lIdx] - 'A']++;
                    else if (diff[g[rIdx] - 'A'] >= 0)
                    {
                        diff[g[rIdx] - 'A']++;
                    }
                    rIdx++;
                }
            }
            return min;
        }
        static int palindromeIndex(string s) // find exactly 1 char index to remove to make the string a palindrome
        {
            int cnt = 0, found = -1;
            for (int lIdx = 0, rIdx = s.Length - 1; lIdx < rIdx; lIdx++, rIdx--)
            {
                if (s[lIdx] != s[rIdx])
                {
                    int lTmp = lIdx, rTmp = rIdx;
                    while (lIdx < rIdx && s[lIdx + 1] == s[rIdx] && s[lIdx] == s[rIdx - 1]) 
                    { 
                        lIdx++; rIdx--; 
                    }
                    if (s[lIdx + 1] == s[rIdx])
                    {
                        found = lTmp;
                        lIdx++;
                    }
                    else if (s[lIdx] == s[rIdx - 1])
                    {
                        found = rTmp;
                        rIdx--;
                    }
                    else return -1;
                    cnt++;
                }
            }
            return cnt == 1 ? found : -1;
        }
        public static List<int> longestCommonSubsequence(List<int> a, List<int> b)
        {
            int[] cmn = new int[1001];
            a.Aggregate(cmn, (c, e) => { if (c[e] == 0) c[e] = 1; return c; });
            b.Aggregate(cmn, (c, e) => { if (c[e] == 1) c[e] = 2; return c; });
            a = a.Where(e => cmn[e] == 2).ToList(); b = b.Where(e => cmn[e] == 2).ToList();
            if (Math.Min(a.Count, b.Count) < 2) return a.Count <= b.Count ? a : b;
            int[,] lcs = new int[a.Count, b.Count];
            int idxA = 0, idxB = 0;
            foreach (int i in a)
            {
                idxB = 0;
                foreach (int j in b)
                {
                    int delta = i == j ? 1 : 0;
                    lcs[idxA, idxB] = idxA == 0 && idxB == 0 ? delta : idxA == 0 ? Math.Max(lcs[0, idxB - 1], delta) : idxB == 0 ? Math.Max(lcs[idxA - 1, 0], delta) : delta == 1 ? lcs[idxA - 1, idxB - 1] + 1 : Math.Max(lcs[idxA - 1, idxB], lcs[idxA, idxB - 1]); 
                    idxB++;
                }
                idxA++;
            }
            List<int> res = new List<int>(); idxA--; idxB--;
            while (idxA >= 0 && idxB >= 0 && lcs[idxA, idxB] > 0)
            {
                if (idxA > 0 && lcs[idxA, idxB] == lcs[idxA - 1, idxB]) idxA--;
                else if (idxB > 0 && lcs[idxA, idxB] == lcs[idxA, idxB - 1]) idxB--;
                else
                {
                    res.Insert(0, a.ElementAt(idxA));
                    idxA--; idxB--;
                }
            }
            return res;
        }
        
        public static long strangeCounter(long t, int a = 3, int r = 2)
        {// Sum of geometric sequence S(n) = a*(r.Pow(n)-1)/(r-1)
            int n = (int)Math.Floor(Math.Log(t / a * (r - 1) + 1, r));
            if (t == a * (Math.Pow(r, n) - 1) / (r - 1)) n--;
            return (long)(a * Math.Pow(r, n) - (t - a * (Math.Pow(r, n) - 1) / (r - 1) - 1));
        }
        public static long mandragora(List<int> H)
        {
            int len = H.Count + 1;
            long sum = 0L;
            return H.OrderByDescending(h => h).Aggregate(0L, (ret, cur) => {
                sum += cur;
                long tmp = sum * (--len);
                if (tmp > ret)
                    ret = tmp;
                return ret;
            });
        }
    }
}
