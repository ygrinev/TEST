using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankCore.Helpers
{
    public class IntOp
    {
        public static long GCD(long a, long b)
        {
            return a == 0 ? b : GCD(b % a, a);
        }

        public static int GCD(int a, int b)
        {
            int x = Math.Min(a, b), y = Math.Max(a, b);
            if (x == 1) return 1;
            int mod = y % x;
            return mod == 0 ? x : GCD(mod, x);
        }
    }
}
