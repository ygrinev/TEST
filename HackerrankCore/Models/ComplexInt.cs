using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankCore.Models
{
    class ComplexInt
    {
        public int mod;
        public int a, b;
        public ComplexInt() { a = 1; b = 0; mod = int.MaxValue; }
        public ComplexInt(int _a, int _b, int _mod = int.MaxValue)
        { mod = _mod; a = (_a % mod + mod) % mod; b = (_b % mod + mod) % mod; }
        public static ComplexInt operator *(ComplexInt x, ComplexInt y)
            => new ComplexInt((int)(((long)x.a * y.a - (long)x.b * y.b) % x.mod),
                              (int)(((long)x.a * y.b + (long)x.b * y.a) % x.mod), x.mod);
    }
}
