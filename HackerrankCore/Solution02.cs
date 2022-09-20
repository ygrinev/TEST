using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankCore
{
    partial class Solution
    {
        // The XOR is simple enough. The trick is that they haven't given us the
        // numbers to XOR, but THEIR POSITION IN THE LIST. To find the actual
        // numbers, determine the number of numbers up to N bits long in the list.
        // Count 0, since we can add any new bit to zero.
        // Then there are 2 numbers with up to 1 bit, 3 with up to 2 bits, etc.
        // To add the numbers with N bits, add the number of numbers with N-2 bits
        // or less to the previous total (the numbers for N-1 bits).
        // The result is the fibonacci sequence, since I just keep adding the last
        // two numbers. And so the number I want is the Zeckendorf representation
        // of the given index, reinterpreted as binary (repeatedly subtracting the
        // largest Fibonacci number available from the index)
        //
        // I've got one case here that is failing, probably just barely failing the time limit.
        // Let's try precalculating the powers as the small speedup needed.

        static int ChandrimaAndXOR(IEnumerable<long> a)
        {
            ulong[] fib = new ulong[100]; fib[0] = 1; fib[1] = 2;
            int i = 2;
            ulong max = (ulong)a.Max(), nextFib = 3;
            // fill out fibonachi seq
            while (nextFib <= max) { nextFib = fib[i - 1] + fib[i - 2]; fib[i++] = nextFib; }
            // fill out bitwise future result
            var pow2 = a.Aggregate(Enumerable.Repeat(false, i).ToArray(), (xor, next)=> { zen(fib, xor, (ulong)next); return xor; });
            i = pow2.Length;
            ulong ret = 0;
            int idx = 0;
            ulong pow2_60_mod1000000007 = 536_396_504, mod = 1; // = 2^60 mod 1000000007, 2^61%1000000007=(536_396_504*2)%1000000007
            while (i > 0)
            {
                ret = (ret + mod*(Convert.ToUInt64(string.Join("", pow2.Skip((idx++) * 60).Take(60).Reverse().Select(b=>b?"1":"0")), 2)%1000000007)%1000000007)%1000000007;
                mod = (mod * pow2_60_mod1000000007) % 1000000007;
                i -= 60;
            }
            return (int)ret; // pow2.Aggregate((ulong)0, (ret, next)=>(ret + poq2mod(pow2[i], ref i))%1000000007);
        }
        private static void zen(ulong[] fibs, bool[] pow2, ulong next)
        {
            if (next < 1) return;
            int idx = fibs.TakeWhile(n => n <= next && n > 0).Count();
            do
            {
                if(fibs[--idx] <= next)
                {
                    pow2[idx] = !pow2[idx];
                    next -= fibs[idx];
                }
            }
            while (next > 0);
        }
        //import os
        //import sys

        //def zeck(n):
        //    res = 0
        //    for i in range(f - 1, -1, -1) :
        //        if n >= fib[i]:
        //            n -= fib[i]
        //            res += p2[i]
        //    return res

        //n = int(input())
        //nums = list(map(int, input().strip().split()))
        //fib = [1, 2, 3]
        //while fib[-1] < max(nums) :
        //    fib.append(fib[-1] + fib[-2])
        //f = len(fib)
        //p2 = []
        //for j in range(f) :
        //    p2.append(1<<j)
        //sol = 0
        //for j in range(n) :
        //    sol ^= zeck(nums[j])
        //print(sol % 1000000007)

        ///
        /// Find first even number of N-row in the pattern:
        ///        1
        //      1  1  1
        //   1  2  3  2  1
        //1  3  6  7  6  3  1
        ///
         int getFirstEven(int n)=> n % 2 == 1 ? 2 : n % 4 == 0 ? 3 : 4;
    }
}
