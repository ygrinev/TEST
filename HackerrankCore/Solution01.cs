using HackerrankCore.Models;
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
    }
}
