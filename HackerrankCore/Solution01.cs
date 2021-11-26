using HackerrankCore.Models;
using HackerrankCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
