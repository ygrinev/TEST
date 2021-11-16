using HackerrankCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankCore
{
    partial class Solution
    {
        public static long GCD(long a, long b)
        {
            return a == 0 ? b : GCD(b % a, a);
        }

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
            return GCD(a, b) == GCD(x, y) ? "YES" : "NO";
        }

    }
}
