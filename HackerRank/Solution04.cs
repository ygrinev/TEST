using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HackerRank
{
    class _Node
    {
        int level;
        public string data;
        public _Node(string _data, int _level)
        {
            this.data = _data;
            level = _level;
        }
        List<_Node> siblings = new List<_Node>();
        public static void Traverse(_Node n, StringBuilder sb, int level)
        {
            sb.Append($"{string.Join("", Enumerable.Repeat("-", 2 + level))}{n.data}/n");
            n.siblings.OrderBy(s => s.data).Aggregate(sb, (bld, s) => {
                Traverse(s, sb, level + 1);
                return sb;
            });
        }
        public void Add(string path)
        {
            IEnumerable<string> sArr = path.Split('/').Where(f => !string.IsNullOrEmpty(f));
            Add(sArr);
        }
        public void Add(IEnumerable<string> path)
        {
            string toAdd = path.ElementAt(0);
            if (data == toAdd)
                siblings.Add(new _Node(toAdd, level+1));
            if (path.Count() > 0)
                siblings.ForEach(s=>s.Add(path.Skip(1)));
        }
    }

    partial class Solution
    {
        //public static string BuildFileTree(List<string> files)
        //{
        //    Node root = new _Node("root/n",0);
        //    StringBuilder sb = new StringBuilder();
        //    files.Aggregate(sb, (bld, p) => {
        //        root.Add(p);
        //    });
        //    _Node.Traverse(root, sb, 0);
        //    return sb;
        //}
        private static BigInteger[] fctrs = new BigInteger[1000];
        private static BigInteger fctrl(int n)
        {
            if (fctrs[0] == 0) // fill out first time
                for (int i = 0; i < 1000; i++)
                {
                    fctrs[i] = i == 0 ? 1 : fctrs[i - 1] * i;
                }
            return fctrs[n];
        }
        public static int[] solveAllSubSets(int n)
        {
            var ret = new int[n + 1];
            for (int i = 0; i < (n + 2) / 2; i++)
            {
                ret[i] = ret[n - i] = (int)((fctrl(n) / fctrl(n - i) / fctrl(i)) % 1000000000);
            }
            // this is more OOD but slower way
            // List<int> half = Enumerable.Repeat(0, (n + 2) / 2).Select((k, i) => (int)((fctrl(n) / fctrl(n - i) / fctrl(i))%1000000000)).ToList();
            // List<int> rev = new List<int>(); rev.AddRange(half);
            // rev.Reverse();
            // half.AddRange(rev.Skip(n % 2 == 0 ? 1 : 0));

            // return half;
            return ret;
        }
        public static int solveKofN(int n, int k) // K-Candy Store: # of ways to select k from n types of candies
        {
            return (int)((fctrl(n + k - 1) / fctrl(n - 1) / fctrl(k)) % 1000000000);
        }
        public static long MaxCmnDiv(long a, long b) { return a % b == 0 ? b : MaxCmnDiv(b, a % b); } // fill tank a or b with water amount c
        public static string solve3Volumes(int a, int b, int c) { return c <= Math.Max(a, b) && c % MaxCmnDiv(a, b) == 0 ? "YES" : "NO"; }
        public static long solveFloatingRocks(KeyValuePair<long, long> c)
        {
            long a = Math.Min(c.Key, c.Value), b = Math.Max(c.Key, c.Value);
            return a == 0 ? Math.Max(b - 1, 0) : b == 0 ? Math.Max(a - 1, 0)
            : MaxCmnDiv(a, b) - 1;
        }
        // (m+n-2)!/((n-1)!* (m-1)!)
        //private static long[] fctrsMod = new long[2000000];
        //private static long fctrlMod(int n, int mod = 1000000007)
        //{
        //    if (fctrsMod[0] == 0) // fill out first time
        //        for (int i = 0; i < 2000000; i++)
        //        {
        //            fctrsMod[i] = i == 0 ? 1 : (fctrsMod[i - 1] * i)%mod;
        //        }
        //    return fctrsMod[n];
        //}
        public static int solvePascal(int n, int m)
        {
            BigInteger fctrlTop = fctrl(m + n - 2);
            return Math.Min(n, m) < 2 ? 1 : (int)(( fctrlTop / fctrl(n - 1) / fctrl(m - 1)) % 1000000007);
        }
        public static double solveCoordsDiam(List<List<int>> coords)
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            int[] cnts = coords.Aggregate(new int[2], (p, c) => {
                int x = c[0], y = c[1];
                if (x == 0) { p[1]++; if (y < minY) minY = y; if (y > maxY) maxY = y; }
                if (y == 0) { p[0]++; if (x < minX) minX = x; if (x > maxX) maxX = x; }
                return p;
            });
            long dmX = maxX - minX, dmY = maxY - minY,
                maxAbsX = Math.Max(Math.Abs(minX), Math.Abs(maxX)),
                maxAbsY = Math.Max(Math.Abs(minY), Math.Abs(maxY));
            return cnts[0] == 0 ? (double)dmY : cnts[1] == 0 ? (double)dmX : new List<double> { dmX, dmY, Math.Sqrt(maxAbsX * maxAbsX + maxAbsY * maxAbsY) }.Max();
        }
        public static List<string> solveLexOrder(List<char> arr)
        {
            var sorted = arr.OrderBy(c => c).ToList();
            return getSubsets(sorted).Select(sb=>sb.ToString()).Cast<string>().ToList();
        }
        private static List<StringBuilder> getSubsets(List<char> sorted)
        {
            if (sorted.Count < 1) return new List<StringBuilder>();
            var res = new List<StringBuilder> { new StringBuilder(sorted[0].ToString()) };
            res.AddRange(getSubsets(sorted.Skip(1).ToList()).Select(sb => new StringBuilder(sorted[0].ToString()).Append(sb)));
            res.AddRange(getSubsets(sorted.Skip(1).ToList()));
            return res;
        }
    }
}
