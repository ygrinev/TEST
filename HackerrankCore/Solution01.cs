﻿using HackerrankCore.Models;
using HackerrankCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HackerrankCore
{
    partial class Solution
    {
        public static List<int> solveRussPeas(int a, int b, long k, int m)
        {
            var p = powRussPeas(new ComplexInt(a, b, m), k);
            return new List<int> { p.a, p.b };
        }
        public static ComplexInt powRussPeas(ComplexInt x, long k)
        {
            ComplexInt p = k % 2 == 0 ? new ComplexInt(1, 0, x.mod) : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static string solveReachFromTo(long a, long b, long x, long y) // is it possible to get from pt(a,b) to pt(x,y) ?
        {
            // (a,b) => (a±b,b) or (a,b±a)
            return IntOp.GCD(a, b) == IntOp.GCD(x, y) ? "YES" : "NO";
        }
        public static int powRussPeas(int x, long k)
        {
            int p = k % 2 == 0 ? 1 : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static BigInteger powRussPeas(BigInteger x, BigInteger k)
        {
            BigInteger p = k % 2 == 0 ? 1 : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static long powRussPeasMod(int x, long k, int mod = 0)
        {
            long p = k % 2 == 0 ? 1 : x;
            while ((k >>= 1) > 0)
            {
                x *= x; if (mod > 0) x %= mod;
                if (k % 2 == 1)
                    p *= x; if (mod > 0) p %= mod;
            }
            return p;
        }
        public static int closestNumber(int a, int b, int x)
        {
            int p = b >= 0 ? powRussPeas(a, b) : a == 1 ? 1 : 0;
            int mod = p % x;
            return p == 0 ? 0 : p >= x ? (mod < x - mod ? (p - mod) : (p+x-mod)) : p < x - p ? 0 : x;
        }
        public static string minGCD(List<int> a) // detect if there exists not empty subset with GCD == 1
        {
            var dist = a.Distinct();
            if (dist.Count() == 1) return dist.ElementAt(0) > 1 ? "NO" : "YES";
            return dist.OrderBy(d => d).Aggregate((cur, next) => IntOp.GCD(cur, next)) == 1 ? "YES" : "NO";
        }
        public static int getModInverse(int a, int mod, int r = 1) // a and mod - coprimes
        {
            if (a > mod) a %= mod;
            int b = mod % a, fctr = ((mod / a + 1)*r)%mod;
            if (b == a - 1) return fctr;
            else if (b == 1) return (int)((mod / a * (long)(mod - 1)) % mod);
            int newA = a-b;
            return getModInverse(newA, mod, fctr);
        }
        public static int gcdExtended(int a, int b, out int x, out int y)
        {
            // Base Case
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            // To store results of recursive call
            int gcd = gcdExtended(b % a, a, out int x1, out int y1);

            // Update x and y using results of recursive call
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
        public static int solvePowMod(int a, int b, int x)
        {
            int fa = a;
            if (b < 0)
            {
                int gcd = gcdExtended(a, x, out fa, out int fb);
                fa = fa < 0 ? fa + x : fa;
            }
            return (int)BigInteger.ModPow(fa, Math.Abs(b), x); // powRussPeas(fa, b, x);
        }
        public static long[] fibs = new long[2000];
        public static int lastIdx = 0;
        public static string isFibo(long n)
        {
            if (lastIdx == 0) fibs[++lastIdx] = 1;
            int prevIdx = lastIdx;
            while (fibs[lastIdx] < n)
            {
                fibs[++lastIdx] = fibs[lastIdx - 1] + fibs[lastIdx - 2];
            }
            if (fibs[lastIdx] == n) return "IsFibo";
            if (prevIdx == lastIdx)
            {
                int left = 0, right = lastIdx;
                do
                {
                    if (fibs[left] == n || fibs[right] == n) return "IsFibo";
                    int mid = (left + right) / 2;
                    if (fibs[mid] < n) left = mid;
                    else right = mid;
                }
                while (left < right - 1);
                if (fibs[left] == n || fibs[right] == n) return "IsFibo";
            }
            return "IsNotFibo";
        }
        private static int ConvertToBase10(List<int> pr)
        {
            int bs = pr.ElementAt(0), nm = pr.ElementAt(1);
            if (bs < 10 && nm.ToString().Any(c => c - '0' >= bs)) return -1;
            long pow = 1;
            long ret = 0;
            while (nm > 0)
            {
                ret += (nm % 10) * pow;
                pow *= bs;
                nm /= 10;
            }
            return (int)ret;
        }
        public static long solveDateJoke(List<List<int>> dates)
        {
            return (long)dates.Select(pr => ConvertToBase10(pr)).Where(n => n >= 0).GroupBy(k => k).Select(g => (long)g.Count() * ((long)g.Count() - 1) / 2).Aggregate(0L, (sum, cur) => sum + cur);
        }
        private static List<int> CalcNext(ref int prev, List<int> lst, int cur)
        {
            int gcd = IntOp.GCD(prev, cur);
            lst.Add((prev / gcd) * (cur / gcd) * gcd);
            prev = cur;
            return lst;
        }
        public static List<int> solveJohnGCD(List<int> a)
        {
            int prev = a.ElementAt(0), prevB = prev;
            List<int> ret = a.Skip(1).Aggregate(new List<int> { prev }, (lst, cur) => CalcNext(ref prev, lst, cur)).ToList();
            ret.Add(a.Last());
            return ret;
        }
        private static int[] quads = new int[498].AsEnumerable().Select((n, i) => 4 * (i + 1) * (i + 1)).ToArray();
        private static bool contains(int div)
        {
            int left = 0, right = 497;
            do
            {
                if (quads[left] == div || quads[right] == div) return true;
                int mid = (left + right) / 2;
                if (quads[mid] < div) left = mid;
                else right = mid;
            }
            while (left < right - 1);
            return quads[left] == div || quads[right] == div;
        }
        private static int getDivs(int n, out int evn)
        {
            int cnt = 1;
            evn = 0;
            for (int i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    cnt += n / i == i ? 1 : 2;
                    if(contains(i)) evn++;
                    if(n / i != i && contains(n/i)) evn++;
                }
            }
            return cnt;
        }
        public static string solveEvenQuadDiv(int n)
        {
            if (n < 8 || n % 4 != 0) return "0";
            int tot = getDivs(n, out int evn);
            int gcd = IntOp.GCD(evn, tot);
            return $"{evn/gcd}/{tot/gcd}";
        }
        private static string calcRow(string cur, string prev, string prev2 = null)
        {
            int len = (cur ?? "").Length;
            return string.Join("", prev.Select((c, i) => c == 'O' || (i > 0 && prev[i - 1] == 'O') || (i < len - 1 && prev[i + 1] == 'O') || cur[i] == 'O' || (prev2 != null && prev2[i] == 'O') ? '.' : 'O'));
        }
        private static List<string> afterExpls(List<string> grid)
        {
            int cnt = 0, len = grid[0].Length, sz = grid.Count;
            string prev = new string('.', len), prev2 = new string('.', len);
            return grid.Aggregate(new List<string>(), (lst, cur) => {
                if (cnt++ > 0) lst.Add(calcRow(cur, prev, cnt > 1 ? prev2 : null));
                if (cnt == sz) lst.Add(calcRow(prev, cur));
                prev2 = prev; prev = cur;
                return lst;
            });
        }
        public static List<string> bomberMan(int n, List<string> grid)
        {
            if (n < 2) return grid;
            if (n % 2 == 0) return grid.Select(s => new string('O', s.Length)).ToList();
            List<string> res3 = afterExpls(grid), res5 = afterExpls(res3);
            return n % 4 == 3 ? res3 : res5;
        }
    }
}
