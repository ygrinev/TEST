using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HackerRank
{
    partial class Solution
    {
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
    }
}
