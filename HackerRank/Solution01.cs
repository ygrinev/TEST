using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return prices.OrderBy(p => p).Aggregate(0, (cnt, n) => {
                if (sum + n <= k)
                {
                    sum += n;
                    cnt++;
                }
                return cnt;
            });
        }

    }
}
